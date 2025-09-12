using UnityEngine;
using System.Collections.Generic;

using Game.Runtime.Core.Command;
using Game.Runtime.Features.Unit;
using Game.Runtime.Inputs.Handlers;

namespace Game.Runtime.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private UnitBase _unit;
        private KeyboardHandler           _input;

        private Queue<ICommand> _enqueueBuffer = new();
        private Queue<ICommand> _consumeBuffer = new();

        private void Awake()
        {
            _input = new KeyboardHandler();
        }

        private void Update()
        {
            _input.PollAndEmit(this);
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
    }
}