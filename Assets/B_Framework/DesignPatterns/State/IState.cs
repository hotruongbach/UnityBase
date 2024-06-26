using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace B_Framework.DesignPatterns.State
{
    public interface IState
    {
        public void Enter();
        public void LogicUpdate();
        public void PhysicUpdate();
        public void Exit();
    }
}
