using System;

namespace Robotech.Money
{
    public interface IMoneyModel : IDisposable
    {
        public int CurrentMoneyAmount { get; set; }
    }
}