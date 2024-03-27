using System;
using UnityEngine;

namespace RedAndBlue.Money
{
    public abstract class MoneyViewBase : MonoBehaviour, IDisposable
    {
        public Transform MoneyItemHolder;
        public GameObject MoneyItemPrefab;
        public abstract void Initialize();
        public abstract void Dispose();
        public abstract void SetMoney(int value, float duration = 0f);

        private void OnDestroy()
        {
            Dispose();
        }
    }
}