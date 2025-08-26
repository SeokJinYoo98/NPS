



using Game.Runtime.Core.Command;
using Game.Runtime.Core.State;

using Game.Features.Unit.State;

using System;

namespace Game.Runtime.Features.Unit.State
{
    public class UnitFsm : IStateMachine
    {
        IState                _currState;
        readonly IUnitContext _unit;

        public UnitFsm(IUnitContext unit, StateType state)
        {
            _unit = unit;
            _currState = CreateState(state);
            _currState.OnEnter(_unit);
        }

        public bool HandleCmd(ICommand cmd)
        {
            var (res, next) = _currState.HandleCmd(_unit, cmd);

            if (res == CmdResult.None) return false;
            if (res == CmdResult.Rejected) return false;
            if (res == CmdResult.Consumed) return true;

            if (res == CmdResult.Transition)
            {
                if (!ChangeState(next)) return false;

                var (res2, _) = _currState.HandleCmd(_unit, cmd);
                return res2 == CmdResult.Consumed || res2 == CmdResult.Transition;
            }

            return false;
        }

        public void UpdateState(float dt)
        {
            // [[수정 필요]]
            (var r, var t) = _currState.OnUpdate(_unit, dt);
            if (r == CmdResult.Transition && t == StateType.Idle)
                ChangeState(StateType.Idle);
        }

        private bool ChangeState(StateType next)
        {
            if (_currState.Type == next) return false;

            _currState.OnExit(_unit);
            _currState = CreateState(next);
            _currState.OnEnter(_unit);

            return true;
        }
        private IState CreateState(StateType state)
            => state switch
            {
                StateType.Idle => new IdleState(),
                StateType.Move => new MoveState(),
                StateType.Attack => new AttackState(),
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
    }
}