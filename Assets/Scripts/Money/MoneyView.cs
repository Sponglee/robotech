using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Robotech.Money
{
    public class MoneyView : MoneyViewBase
    {
        [SerializeField] private TMP_Text _moneyText;

        private Tweener _moneyTextTweener;
        private int _cachedValue;

        public override void Initialize()
        {
        }

        public override void Dispose()
        {
            _moneyTextTweener?.Kill();
            _moneyTextTweener = null;
            _cachedValue = default;
        }

        public override void SetMoney(int value, float duration = 0f)
        {
            PlayMoneyAnimation(value, duration);
        }

        private void PlayMoneyAnimation(int value, float duration)
        {
            _moneyTextTweener?.Kill();

            _moneyTextTweener = DOVirtual.Int(
                    _cachedValue, value, duration, UpdateMoneyText)
                .SetEase(Ease.OutQuad);
        }

        private void UpdateMoneyText(int value)
        {
            _cachedValue = value;
            _moneyText.text = value.ToString();
        }
    }
}