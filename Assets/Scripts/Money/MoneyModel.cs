namespace Robotech.Money
{
    public class MoneyModel : IMoneyModel
    {
        public int CurrentMoneyAmount { get; set; }

        public MoneyModel(int moneyAmount)
        {
            CurrentMoneyAmount = moneyAmount;
        }

        public void Dispose()
        {
        }
    }
}