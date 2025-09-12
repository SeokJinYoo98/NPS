using UnityEngine;
using Game.Runtime.Core.Command;

namespace Game.Runtime.Features.Unit
{
    public abstract class UnitBase : MonoBehaviour, ICommandable
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual bool HandleCommand(in ICommand cmd)
        {
            throw new System.NotImplementedException();
        }

    }
}
// �⺻ �Է� 1, 2, 3, wasd

