using System;
using UnityEngine;

namespace RedAndBlue.Money
{
    public interface IMoneyPresenter : IDisposable
    {
        public void Initialize();
        public void AddMoney(int amount);
        public void UpdateMoneyVisual();
        public bool TryTakeMoney(int amount);
        public IMoneyItemPresenter SpawnCoins(Vector3 position);
        public void ReturnToPool(IMoneyItemPresenter moneyItemPresenter);
    }
}