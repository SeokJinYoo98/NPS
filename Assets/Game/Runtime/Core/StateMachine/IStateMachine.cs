using UnityEngine;
using Game.Runtime.Core.Command;

namespace Game.Runtime.Core.State
{
    public enum StateType { None, Idle, Move, Attack };
    public interface IStateMachine
    {
        void UpdateState(float dt);
        bool HandleCmd(Command.ICommand cmd);
    }

    public interface IState
    {
        StateType   Type { get; }
        void        OnEnter(IUnitContext ctx);
        void        OnExit(IUnitContext ctx);
        (CmdResult, StateType) OnUpdate(IUnitContext ctx, float dt);
        (CmdResult, StateType) HandleCmd(IUnitContext ctx, ICommand cmd);
    } 
    public interface IUnitContext
    {
        void SetDir(Vector2 dir);
        void Attack();
    }
}
