using UnityEngine;
using Game.Runtime.Core.Command;
using Game.Runtime.Core.State;
using Game.Runtime.Features.Unit.State;

namespace Game.Runtime.Features.Unit.Type
{
    public class PlayerUnit : UnitBase, IUnitContext
    {
        IStateMachine _fsm;
        void Awake()
        {
            _fsm = new UnitFsm(this, StateType.Idle);
        }
        void FixedUpdate()
        {
            _fsm.UpdateState(Time.fixedDeltaTime);
        }
        public override bool HandleCommand(in ICommand cmd)
        {
            return _fsm.HandleCmd(cmd);
        }

        public void Attack()
        {
            Debug.Log("Char Attack");
        }

        public void SetDir(Vector2 dir)
        {
            Debug.Log($"MoveCmd Execute = {dir.x}, {dir.y}");
        }

    }
}
