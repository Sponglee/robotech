using System;

namespace Robotech.Buttons
{
    public class ButtonsProvider : IDisposable
    {
        public event Action<IButtonPresenter> ButtonClickedEvent;

        private readonly ButtonData[] _buttonDatas;

        private IButtonPresenter[] _buttons;

        public ButtonsProvider(ButtonData[] buttonDatas)
        {
            _buttonDatas = buttonDatas;
        }

        public void Initialize()
        {
            var buttonsCount = _buttonDatas.Length;
            _buttons = new IButtonPresenter[buttonsCount];

            for (var i = 0; i < _buttonDatas.Length; i++)
            {
                var buttonData = _buttonDatas[i];

                _buttons[i] = InitializeButton(buttonData);
            }
        }

        public void Dispose()
        {
            for (var i = 0; i < _buttons.Length; i++)
            {
                var button = _buttons[i];
                button.UnSubscribeOnButtonPressed(OnButtonClicked);
                button.Dispose();
            }
        }

        public void ActivateOneButton(ButtonType type)
        {
            for (var i = 0; i < _buttons.Length; i++)
            {
                var button = _buttons[i];
                var buttonType = button.GetButtonType();

                if (buttonType == type)
                {
                    button.TryActivateButton();
                }
            }
        }

        public void DeactivateAll()
        {
            for (var i = 0; i < _buttons.Length; i++)
            {
                var button = _buttons[i];

                button.ToggleButton(false);
            }
        }

        private IButtonPresenter InitializeButton(ButtonData buttonData)
        {
            var buttonModel = new ButtonModel(buttonData);
            var buttonPresenter = new ButtonPresenter(buttonData.ButtonView, buttonModel);

            buttonPresenter.SubscribeOnButtonPressed(OnButtonClicked);

            return buttonPresenter;
        }

        private void OnButtonClicked(IButtonPresenter button)
        {
            ButtonClickedEvent?.Invoke(button);
        }
    }
}