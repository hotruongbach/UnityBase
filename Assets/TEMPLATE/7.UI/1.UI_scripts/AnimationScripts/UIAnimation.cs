using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Monster.UI
{
    public abstract class UIAnimation : MonoBehaviour
    {
        protected virtual void OnValidate()
        {
            Window window = GetComponent<Window>();
            if (window != null)
            {
                window.RegistNewUIAnimation(this);
            }
        }

        public abstract void PlayIn(Action onComplete = null);
        public abstract void PlayOut(Action onComplete = null);
    }
}