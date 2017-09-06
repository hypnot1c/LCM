using System;
using System.Collections.Generic;
using System.Text;

namespace LCM.Core.Entity.Cards
{
    public class LoyaltyCard : BaseEntity
    {
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}
