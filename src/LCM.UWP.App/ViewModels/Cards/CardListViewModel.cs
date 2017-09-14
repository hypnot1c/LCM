using LCM.Core.UI.ViewModels.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using LCM.Core.Service;
using Windows.Devices.Enumeration;
using Windows.Media.Capture;
using Windows.Graphics.Display;

namespace LCM.UWP.App.ViewModels.Cards
{
  public class CardListViewModel : CardListBaseViewModel
  {
    public CardListViewModel(IEventAggregator eventAggregator,
      INavigationService navigationService,
      LCMUnitOfWork unitOfWork
      ) : base(eventAggregator, unitOfWork)
    {
      this._navService = navigationService;
    }

    private readonly INavigationService _navService;

    protected override void OnActivate()
    {
      _evAggregator.Subscribe(this);
    }

    protected override void OnDeactivate(bool close)
    {
      _evAggregator.Unsubscribe(this);
    }

    public void LoyaltyCardAdd()
    {
      this._navService.For<CardCreateViewModel>().Navigate();
    }
  }
}
