using API.Core;
using LCM.Core.Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using LCM.Core.Entity.Cards;
using LCM.Data.Master.EF;
using System.Linq;

namespace LCM.Core.Service
{
  public class LCMUnitOfWork : IUnitOfWork, IDataService
  {
    public LCMUnitOfWork(MasterDbContext msrDb)
    {
      this.msrDb = msrDb;
    }

    public MasterDbContext msrDb { get; }

    public void Commit()
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }

    public IEnumerable<LoyaltyCard> GetAllCards()
    {
      var cards = this.msrDb.LoyaltyCards.Select(c => new LoyaltyCard { Id = c.Id, Name = c.Name });
      return cards;
    }

    public void Rollback()
    {
      throw new NotImplementedException();
    }

    public void Save()
    {
      throw new NotImplementedException();
    }
  }
}
