using LCM.Core.UI.ViewModels.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace LCM.UWP.App.ViewModels.Cards
{
    public class CardCreateViewModel : CardCreateBaseViewModel
    {
        public CardCreateViewModel(
            IEventAggregator eventAggregator, 
            INavigationService navigationService
            ) : base(eventAggregator)
        {
            this._navService = navigationService;
        }

        protected INavigationService _navService;

        public override void Save()
        {
            base.Save();
            if (this._navService.CanGoBack)
            {
                this._navService.GoBack();
            }
            else
            {

            }
            //this._navService.For<EventListViewModel>().Navigate();
            //throw new NotImplementedException();
        }

        public void Cancel()
        {
            if (this._navService.CanGoBack)
            {
                this._navService.GoBack();
            }
            else
            {
                //this._navService.For<EventListViewModel>().Navigate();
            }
        }
    }
}
