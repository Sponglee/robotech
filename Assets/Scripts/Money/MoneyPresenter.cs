using System.Collections.Generic;
using UnityEngine;

namespace RedAndBlue.Money
{
    public class MoneyPresenter : IMoneyPresenter
    {
        private const float MoneyUpdateDuration = 0.25f;
        private const int MoneyItemPoolCapacity = 3;

        private readonly IMoneyModel _model;
        private readonly MoneyViewBase _view;

        private List<IMoneyItemPresenter> _moneyItems = new List<IMoneyItemPresenter>();

        public MoneyPresenter(IMoneyModel model, MoneyViewBase view)
        {
            _model = model;
            _view = view;
        }

        public void Initialize()
        {
            _view.SetMoney(_model.CurrentMoneyAmount);

            InitializeMoneyItemPool();
        }

        public void Dispose()
        {
            _view.Dispose();
            _model.Dispose();

            for (var i = 0; i < _moneyItems.Count; i++)
            {
                var item = _moneyItems[i];
                item.Dispose();
            }

            _moneyItems.Clear();
        }

        public void AddMoney(int amount)
        {
            _model.CurrentMoneyAmount += amount;
        }

        public void UpdateMoneyVisual()
        {
            _view.SetMoney(_model.CurrentMoneyAmount, MoneyUpdateDuration);
        }

        public bool TryTakeMoney(int amount)
        {
            var success = true;
            var resultAmount = _model.CurrentMoneyAmount - amount;

            if (resultAmount <= 0)
            {
                success = false;
                resultAmount = 0;
            }

            _model.CurrentMoneyAmount = resultAmount;
            _view.SetMoney(_model.CurrentMoneyAmount, MoneyUpdateDuration);
            return success;
        }

        public IMoneyItemPresenter SpawnCoins(Vector3 position)
        {
            IMoneyItemPresenter moneyItem = null;

            for (var i = 0; i < _moneyItems.Count; i++)
            {
                var tmpItem = _moneyItems[i];
                if (!tmpItem.IsActive())
                {
                    moneyItem = tmpItem;
                    break;
                }
            }

            moneyItem ??= InitializeMoneyItem();

            moneyItem.SetMovementFinishedCallback(() =>
            {
                UpdateMoneyVisual();
                ReturnToPool(moneyItem);
            });

            moneyItem.Initialize(position);
            return moneyItem;
        }

        private void InitializeMoneyItemPool()
        {
            _moneyItems = new List<IMoneyItemPresenter>();

            for (var i = 0; i < MoneyItemPoolCapacity; i++)
            {
                var presenter = InitializeMoneyItem();
                presenter.Reset();
            }
        }

        public void ReturnToPool(IMoneyItemPresenter moneyItemPresenter)
        {
            moneyItemPresenter.SetPosition(Vector3.zero);
            moneyItemPresenter.Reset();
        }

        private IMoneyItemPresenter InitializeMoneyItem()
        {
            var prefab = _view.MoneyItemPrefab;

            var model = new MoneyItemModel();
            var view = Object.Instantiate(prefab, _view.MoneyItemHolder).GetComponent<MoneyItemViewBase>();
            var presenter = new MoneyItemPresenter(model, view);

            _moneyItems.Add(presenter);

            return presenter;
        }
    }
}