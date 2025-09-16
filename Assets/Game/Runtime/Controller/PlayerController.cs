using UnityEngine;
using System.Collections.Generic;

using Game.Runtime.Core.Input;
using Game.Runtime.Core.Command;
using Game.Runtime.Features.Unit;
using Game.Runtime.Features.Movement.Commands;
using Game.Runtime.Features.Combat.Commands;
using Game.Runtime.Inputs.Handlers;

namespace Game.Runtime.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private UnitBase           _unit;
        [SerializeField] private KeyboardHandler    _input;

        private Queue<ICommand> _enqueueBuffer = new();
        private Queue<ICommand> _consumeBuffer = new();

        private void Awake()
        {
 
        }

        private void Update()
        {
            UpdateInput();
        }
        private void FixedUpdate()
        {
            ConsumeCmd();
        }

        public void EnqueueCmd(ICommand cmd) => _enqueueBuffer.Enqueue(cmd);
        private void ConsumeCmd()
        {
            if (_enqueueBuffer.Count == 0) return;
            (_enqueueBuffer, _consumeBuffer) = (_consumeBuffer, _enqueueBuffer);
            _enqueueBuffer.Clear();

            while (0 < _consumeBuffer.Count)
            {
                var cmd = _consumeBuffer.Dequeue();
                _unit.HandleCommand(cmd);
            }
        }
        private void UpdateInput()
        {
            if (_input.TryGetFrame(out var frame))
            {
                EnqueueCmd(new MoveCommand(frame.MoveDir));

                if ((frame.DownBits & (ushort)Btn.LMB) != 0)
                    EnqueueCmd(new AttackCommand());
                
            }
        }

        private bool IsPressed(in InputFrame ip)
        {
            return true;
        }
    }
}