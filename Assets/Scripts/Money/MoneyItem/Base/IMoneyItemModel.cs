using System;

namespace RedAndBlue.Money
{
    public interface IMoneyItemModel : IDisposable
    {
        public bool IsActive { get; set; }
    }
}