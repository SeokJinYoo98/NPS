using UnityEngine;

namespace Game.Runtime.Core.Command
{
    public enum CmdResult { None, Consumed, Rejected, Deferred, Transition }
    public interface ICommandable
    {
        bool HandleCommand(in ICommand cmd);
    }
    public interface ICommand
    {
        int         Id       { get; }
        object      Payload  { get; } // 추후 T, byte[]로 교체
    }
}


