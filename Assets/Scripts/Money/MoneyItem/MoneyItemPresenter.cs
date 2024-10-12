using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Robotech.Money
{
    public class MoneyItemPresenter : IMoneyItemPresenter
    {
        private const float SpreadAmountInPixels = 100f;
        private const float SpreadOutDuration = 0.5f;
        private const float MoveInDuration = 0.5f;

        private readonly IMoneyItemModel _model;
        private readonly MoneyItemViewBase _view;

        private Action _movementFinishedCallback;

        public MoneyItemPresenter(IMoneyItemModel model, MoneyItemViewBase viewBase)
        {
            _model = model;
            _view = viewBase;
        }

        public void Initialize(Vector3 position)
        {
            SetPosition(position);
            _model.IsActive = true;
            _view.gameObject.SetActive(true);
            _view.Initialize(SpreadOutDuration, MoveInDuration);

            var randomPoint = GetRandomPointInCircle(SpreadAmountInPixels);
            var spreadPosition = _view.transform.position + randomPoint;
            _view.StartMoveToHolder(spreadPosition, _movementFinishedCallback);
        }

        public void Dispose()
        {
            Reset();
            _view.Dispose();
        }

        public void Reset()
        {
            _model.IsActive = false;
            _movementFinishedCallback = null;
        }

        public void SetPosition(Vector3 position)
        {
            _view.transform.position = position;
        }

        public bool IsActive()
        {
            return _model.IsActive;
        }

        public void SetMovementFinishedCallback(Action callback)
        {
            _movementFinishedCallback = callback;
        }

        private Vector3 GetRandomPointInCircle(float radius)
        {
            var angle = Random.Range(0f, 360f);

            var distance = Mathf.Sqrt(Random.value) * radius;

            var x = distance * Mathf.Cos(angle);
            var y = distance * Mathf.Sin(angle);

            return new Vector3(x, y, 0f);
        }
    }
}