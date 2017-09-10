using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using LCM.Core.Entity.Cards;
using System.Collections.ObjectModel;
using LCM.Core.Service;

namespace LCM.Core.UI.ViewModels.Cards
{
  public class CardListBaseViewModel : BaseViewModel
  {
    public CardListBaseViewModel(
      IEventAggregator eventAggregator,
      LCMUnitOfWork unitOfWork
      ) : base(eventAggregator)
    {
      this._unitOfWork = unitOfWork;
      var cards = this._unitOfWork.GetAllCards();
      this.LoyaltyCards = new BindableCollection<LoyaltyCard>(cards);
    }

    private readonly LCMUnitOfWork _unitOfWork;

    public BindableCollection<LoyaltyCard> LoyaltyCards { get; set; }
  }
}
