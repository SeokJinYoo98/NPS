using UnityEngine;
using Game.Runtime.Core.Command;


namespace Game.Runtime.Features.Movement.Commands
{
    public class MoveCommand : ICommand
    {
        private readonly Vector2 _dir;
        public int Id => 1;
        public object Payload => _dir;
        public MoveCommand(Vector2 dir)
            => _dir = dir;
    }
}