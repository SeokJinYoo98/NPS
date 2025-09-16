using UnityEngine;
using Game.Runtime.Core.Input;
using UnityEngine.InputSystem;
using NUnit.Framework;

namespace Game.Runtime.Inputs.Handlers
{
    public enum PlayerActionType { Move, Look, LeftClick, RightClick };
    public class KeyboardHandler : MonoBehaviour, IInputHandler
    {
        private PlayerInput _input;
        private InputAction _move, _look;
        private InputBind[] _binds;

        private ushort _prevBits;
        void Awake()
        {
            TryGetComponent<PlayerInput>(out _input);
            var map = _input.actions.FindActionMap("PlayerActions");

            _move = map.FindAction("Move");
            _look = map.FindAction("Look");

            _binds = new InputBind[2]
            {
                new InputBind(map.FindAction("LMB"), Btn.LMB),
                new InputBind(map.FindAction("RMB"), Btn.RMB)
            };
        }
        void OnEnable()
        {
            _move.Enable(); _look.Enable();
            for (int i = 0; i < _binds.Length; ++i)
                _binds[i].Act.Enable();
        }
        void OnDisable()
        {
            _move.Disable(); _look.Disable();
            for (int i = 0; i < _binds.Length; ++i)
                _binds[i].Act.Disable();
        }

        public bool TryGetFrame(out InputFrame frame)
        {
            // 1) 값형 액션 폴링
            Vector2 mv = _move.ReadValue<Vector2>();

            // 2) 버튼 폴링→비트 스냅샷
            ushort curr = 0;
            for (int i = 0; i < _binds.Length; ++i)
                if (_binds[i].Act.IsPressed())
                    curr |= (ushort)_binds[i].Bit;

            // 3) Down 계산
            ushort down = (ushort)(curr & ~_prevBits);

            // 4) 프레임 구성
            frame = new InputFrame
            {
                MoveDir  = mv,
                HeldBits = curr,
                DownBits = down,
            };

            _prevBits = curr;
            return true;
        }
    }
} 