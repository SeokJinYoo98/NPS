using UnityEngine;

using Game.Runtime.Core.Command;
using Game.Runtime.Core.State;

namespace Game.Features.Unit.State
{
    public class MoveState : IState
    {
        Vector2 _dir;
        float _testTime = 0.6f;
        bool _testV = false;
        public StateType Type => StateType.Move;

        public (CmdResult, StateType) HandleCmd(IUnitContext ctx, ICommand cmd)
        {
            if (cmd.Id == 1)
            {
                var dir = (Vector2)cmd.Payload;
                if (dir.sqrMagnitude <= 0.01f)
                    return (CmdResult.Transition, StateType.Idle);

                _dir = dir;
                ctx.SetDir(dir);
                return (CmdResult.Consumed, StateType.Move);
            }
            if (cmd.Id == 2)
                return (CmdResult.Transition, StateType.Attack);
            return (CmdResult.None, StateType.None);
        }

        public void OnEnter(IUnitContext ctx)
        {
            Debug.Log("Move State OnEnter");
        }

        public void OnExit(IUnitContext ctx)
        {
            Debug.Log("Move State OnExit");
        }

        public (CmdResult, StateType) OnUpdate(IUnitContext ctx, float dt)
        {
            if (_testTime >= 0f)
                _testTime -= dt;
            else
            {
                _testTime += 1f;
                if (!_testV)
                {
                    Debug.Log("Move State OnUpdate");
                    Debug.Log($"Dir: {_dir.x}, {_dir.y}");
                    _testV = true;
                }

            }
            return (CmdResult.None, StateType.None);
        }

    }
    public class IdleState : IState
    {
        float _testTime = 1f;
        public StateType Type => StateType.Idle;
        bool _testV = false;

        public void OnEnter(IUnitContext ctx)
        {
            ctx.SetDir(Vector2.zero);
            Debug.Log("Idle State OnEnter");
        }

        public void OnExit(IUnitContext ctx)
        {
            Debug.Log("Idle State OnExit");
        }

        public (CmdResult, StateType) OnUpdate(IUnitContext ctx, float dt)
        {
            if (_testTime >= 0f)
                _testTime -= dt;
            else
            {
                _testTime += 0.6f;
                if (!_testV)
                {
                    Debug.Log("Idle State OnUpdate");
                    _testV = true;
                }
                
            }
            return (CmdResult.None, StateType.None);
        }
        public (CmdResult, StateType) HandleCmd(IUnitContext ctx, ICommand cmd)
        {
            if (cmd.Id == 1)
            {
                var dir = (Vector2)cmd.Payload;
                if (dir.sqrMagnitude <= 0.01f)
                    return (CmdResult.Consumed, this.Type);


                return (CmdResult.Transition, StateType.Move);
            }
            if (cmd.Id == 2)
                return (CmdResult.Transition, StateType.Attack);

            return (CmdResult.None, StateType.None);
        }
    }
    public class AttackState : IState
    {
        public StateType Type => StateType.Attack;
        float _fixedTime;
        float _cooldown = 0f;
        bool _nextAttack = false;
        bool _coolDownCompleted = true;
        bool _testV = false;
        public (CmdResult, StateType) HandleCmd(IUnitContext ctx, ICommand cmd)
        {
            if (cmd.Id == 1)
            {
                if (_coolDownCompleted && !_nextAttack)
                {
                    return (CmdResult.Transition, StateType.Idle);
                }
            }
            if (cmd.Id == 2)
            {
                if (_coolDownCompleted)
                {
                    ctx.Attack();
                    _fixedTime = _cooldown = (float)cmd.Payload;
                    _coolDownCompleted = false;
                    return (CmdResult.Consumed, StateType.Attack);
                }
                else
                    return (CmdResult.Rejected, StateType.Attack);
            }

            return (CmdResult.None, StateType.None);
        }

        public void OnEnter(IUnitContext ctx)
        {
            Debug.Log("Attack OnEnter");
        }

        public void OnExit(IUnitContext ctx)
        {
            Debug.Log("Attack OnExit");
        }

        public (CmdResult, StateType) OnUpdate(IUnitContext ctx, float dt)
        {
            if (!_coolDownCompleted)
            {

                if (_cooldown >= 0f)
                {
                    if (!_testV)
                    {
                        Debug.Log("°ø°Ý ÈÄµô");
                        _testV = !_testV;
                    }
                    
                    _cooldown -= dt;
                }
                else
                {
                    _cooldown += _fixedTime;
                    _coolDownCompleted = true;
                    return (CmdResult.Transition, StateType.Idle);
                }
            }

            return (CmdResult.None, StateType.None);
        }
    }
}