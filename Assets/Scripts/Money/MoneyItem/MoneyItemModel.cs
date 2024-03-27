namespace RedAndBlue.Money
{
    public class MoneyItemModel : IMoneyItemModel
    {
        public bool IsActive { get; set; }

        public void Dispose()
        {
            IsActive = false;
        }
    }
}