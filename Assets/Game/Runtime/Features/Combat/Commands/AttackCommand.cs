using UnityEngine;
using Game.Runtime.Core.Command;


namespace Game.Runtime.Features.Combat.Commands
{
    public class AttackCommand : ICommand
    {
        private readonly float _waitTime = 0.6f;
        public int Id => 2;
        public object Payload => _waitTime;
    }
}