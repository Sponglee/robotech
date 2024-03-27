using System;
using UnityEngine;

namespace RedAndBlue.Money
{
    public abstract class MoneyItemViewBase : MonoBehaviour, IDisposable
    {
        public abstract void Initialize(float spreadDuration, float moveDuration);
        public abstract void Dispose();
        public abstract void StartMoveToHolder(Vector3 spreadPosition, Action callback);

        private void OnDestroy()
        {
            Dispose();
        }
    }
}