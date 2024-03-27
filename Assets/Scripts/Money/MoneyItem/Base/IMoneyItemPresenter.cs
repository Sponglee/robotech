using System;
using UnityEngine;

namespace RedAndBlue.Money
{
    public interface IMoneyItemPresenter : IDisposable
    {
        public void Initialize(Vector3 position);
        public void Reset();
        public void SetPosition(Vector3 position);
        public bool IsActive();
        public void SetMovementFinishedCallback(Action callback);
    }
}