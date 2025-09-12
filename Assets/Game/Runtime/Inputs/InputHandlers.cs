using UnityEngine;
using Game.Runtime.Controller;
using Game.Runtime.Core.Input;
using Game.Runtime.Core.Command;

using Game.Runtime.Features.Movement.Commands;
using Game.Runtime.Features.Combat.Commands;

namespace Game.Runtime.Inputs.Handlers
{
    public sealed class KeyboardHandler : IInputHandler
    {
        private const float DIR_EPS = 0.001f;
        private Vector2 _prevDir = Vector2.zero;

        public Vector2 GetAxis(string axisName) => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        public void PollAndEmit(PlayerController pc)
        {
            Vector2 dir = GetAxis("Move");
            if ((dir - _prevDir).sqrMagnitude > DIR_EPS)
            {
                _prevDir = dir;
                pc.EnqueueCmd(new MoveCommand(dir));
            }
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("마우스 클릭");
                pc.EnqueueCmd(new AttackCommand());
            }
        }
        public ICommand GetCommand()
        {
            return null;
        }

        public bool IsPressed(KeyCode k)
            => Input.GetKey(k);
    }
} 