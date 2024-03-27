using System;

namespace RedAndBlue.Money
{
    public interface IMoneyModel : IDisposable
    {
        public int CurrentMoneyAmount { get; set; }
    }
}