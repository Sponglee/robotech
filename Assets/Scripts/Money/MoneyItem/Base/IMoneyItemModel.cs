using System;

namespace Robotech.Money
{
    public interface IMoneyItemModel : IDisposable
    {
        public bool IsActive { get; set; }
    }
}