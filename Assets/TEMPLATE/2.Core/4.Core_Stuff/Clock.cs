using System;
using System.Collections;
using UnityEngine;

namespace Monster
{
    public class Clock : MonoBehaviour
    {
        public static EventHandler Tick;
        WaitForSeconds second = new WaitForSeconds(1);
        public IEnumerator Activate()
        {
            yield return null;
            StartCoroutine(TickCount());
        }
        private IEnumerator TickCount()
        {
            while (true)
            {
                yield return second;
                Tick?.Invoke(this, EventArgs.Empty);
            }
        }
        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}