using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using LCM.Core.Entity.Cards;
using System.Collections.ObjectModel;

namespace LCM.Core.UI.ViewModels.Cards
{
    public class CardListBaseViewModel : BaseViewModel
    {
        public CardListBaseViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            this.LoyaltyCards = new BindableCollection<LoyaltyCard>(new List<LoyaltyCard>());
        }

        BindableCollection<LoyaltyCard> LoyaltyCards { get; set; }
    }
}   
