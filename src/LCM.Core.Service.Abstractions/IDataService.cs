using LCM.Core.Entity.Cards;
using System.Collections.Generic;

namespace LCM.Core.Service.Abstractions
{
  public interface IDataService
  {
    IEnumerable<LoyaltyCard> GetAllCards();
  }
}
