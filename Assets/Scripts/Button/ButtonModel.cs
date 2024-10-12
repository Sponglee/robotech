namespace Robotech.Buttons
{
    public class ButtonModel : IButtonModel
    {
        public ButtonType ButtonType { get; }
        public bool IsActive { get; set; }
        public float ActivationTime { get; }
        public int MoneyReward { get; }
        public float ElapsedTime { get; set; }

        public ButtonModel(ButtonData buttonData)
        {
            IsActive = false;
            ButtonType = buttonData.ButtonType;
            MoneyReward = buttonData.RewardAmount;
            ActivationTime = buttonData.ActiveTime;
            ElapsedTime = 0f;
        }

        public void Dispose()
        {
        }
    }
}