using UnityEngine;

namespace Game.Runtime.Core.Input
{
    public interface IInputHandler
    {
        Command.ICommand    GetCommand();
        bool        IsPressed(KeyCode k);
        Vector2     GetAxis(string axisName);
    }
}