using API.Core;
using LCM.Core.Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using LCM.Core.Entity.Cards;
using LCM.Data.Master.EF;
using System.Linq;
using LCM.Data.Master.Model;

namespace LCM.Core.Service
{
  public class LCMUnitOfWork : IUnitOfWork, IDataService
  {
    public LCMUnitOfWork(MasterDbContext msrDb)
    {
      this.msrDb = msrDb;
    }

    protected MasterDbContext msrDb { get; }

    public void Commit()
    {
      if(this.msrDb.Database.CurrentTransaction != null)
      {
        this.msrDb.Database.CommitTransaction();
      }
    }

    public void Dispose()
    {
      this.msrDb.Dispose();
    }


    public void Rollback()
    {
      if (this.msrDb.Database.CurrentTransaction != null)
      {
        this.msrDb.Database.RollbackTransaction();
      }
    }

    public void Save()
    {
      this.msrDb.SaveChanges();
    }


    public IEnumerable<LoyaltyCard> GetAllCards()
    {
      var cards = this.msrDb.LoyaltyCards.Select(c => new LoyaltyCard { Id = c.Id, Name = c.Name, Image = c.Image });
      return cards;
    }

    public void CreateLoyaltyCard(LoyaltyCard card)
    {
      var dbCard = new LoyaltyCardModel { Name = card.Name, Image = card.Image };
      this.msrDb.LoyaltyCards.Add(dbCard);

      card.Id = dbCard.Id;
    }
  }
}
