using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace Game.Runtime.Core.Input
{
    [Flags] public enum Btn : ushort
    {
        None = 0,
        LMB = 1 << 0,
        RMB = 1 << 1
    }
    public struct InputFrame
    {
        public Vector2  MoveDir;
        public bool     IsAttack;
        public ushort HeldBits, DownBits;
  
    }
    public readonly struct InputBind
    {
        public readonly InputAction Act;
        public readonly Btn         Bit;
        public InputBind(InputAction a, Btn b)
        {
            Act = a; Bit = b;
        }
    }
    public interface IInputHandler
    {
        bool TryGetFrame(out InputFrame frame);
    }
}