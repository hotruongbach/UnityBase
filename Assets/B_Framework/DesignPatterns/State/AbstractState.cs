using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace B_Framework.DesignPatterns.State
{
    public abstract class AbstractState<T>
    {
        protected T controller;

        public AbstractState(T controller)
        {
            this.controller = controller;
        }
    }
}