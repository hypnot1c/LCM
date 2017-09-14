using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using LCM.Core.Service;
using LCM.Core.Entity.Cards;

namespace LCM.Core.UI.ViewModels.Cards
{
  public class CardCreateBaseViewModel : BaseViewModel
  {
    public CardCreateBaseViewModel(
      IEventAggregator eventAggregator,
      LCMUnitOfWork unitOfWork
      ) : base(eventAggregator)
    {
      this.unitOfWork = unitOfWork;
    }

    private string _name;
    public string Name
    {
      get { return this._name; }
      set { this._name = value; base.NotifyOfPropertyChange(() => this.Name); }
    }

    private byte[] _image;

    public byte[] Image
    {
      get { return this._image; }
      set { this._image = value; base.NotifyOfPropertyChange(() => this.Image); }
    }

    private readonly LCMUnitOfWork unitOfWork;

    public virtual void Save()
    {
      var card = new LoyaltyCard { Name = this.Name, Image = this.Image };
      this.unitOfWork.CreateLoyaltyCard(card);
      this.unitOfWork.Save();
    }
  }
}
