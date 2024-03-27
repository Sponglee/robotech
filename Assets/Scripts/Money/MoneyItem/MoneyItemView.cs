using System;
using DG.Tweening;
using UnityEngine;

namespace RedAndBlue.Money
{
    public class MoneyItemView : MoneyItemViewBase
    {
        private Sequence _moveSequence;
        private float _spreadOutDuration;
        private float _moveInDuration;

        public override void Initialize(float spreadDuration, float moveDuration)
        {
            _spreadOutDuration = spreadDuration;
            _moveInDuration = moveDuration;
        }

        public override void Dispose()
        {
            _moveSequence?.Kill();
            _moveSequence = null;
        }

        public override void StartMoveToHolder(Vector3 spreadPosition, Action callback)
        {
            _moveSequence?.Kill();
            _moveSequence = DOTween.Sequence();

            _moveSequence.Append(transform.DOMove(spreadPosition, _spreadOutDuration)
                .SetEase(Ease.OutCubic));

            _moveSequence.Append(transform.DOLocalMove(Vector3.zero, _moveInDuration)
                .SetEase(Ease.OutSine)
                .OnComplete(() => callback()));

            _moveSequence.Play();
        }
    }
}