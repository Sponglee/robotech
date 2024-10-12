using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Robotech.Buttons
{
    public class ClickableButtonView : ButtonViewBase
    {
        [SerializeField] private Button _button;
        [SerializeField] private Transform _movableButtonPart;

        private IButtonPresenter _presenter;
        private Sequence _clickSequence;

        private Vector2 _startOffset;
        private float _clickDuration;

        public override void Initialize(IButtonPresenter presenter, Vector2 offset, float clickDuration)
        {
            _button.onClick.AddListener(OnClick);
            _presenter = presenter;
            _startOffset = offset;
            _clickDuration = clickDuration;
            _movableButtonPart.localPosition = _startOffset;
        }

        public override void Dispose()
        {
            _clickSequence?.Kill();
            _clickSequence = null;
            _button.onClick.RemoveListener(OnClick);
        }

        public override void ToggleVisuals(bool toggle)
        {
        }

        private void PlayClickAnimation()
        {
            _clickSequence?.Kill();

            _clickSequence = DOTween.Sequence();

            var halfClickDuration = _clickDuration / 2f;

            _clickSequence.Append(_movableButtonPart.DOLocalMove(Vector3.zero, halfClickDuration));
            _clickSequence.Append(_movableButtonPart.DOLocalMove(new Vector3(_startOffset.x, _startOffset.y, 0f),
                halfClickDuration));

            _clickSequence.Restart();
            _clickSequence.Play();
        }

        private void OnClick()
        {
            PlayClickAnimation();
            _presenter.OnButtonPressed();
        }
    }
}