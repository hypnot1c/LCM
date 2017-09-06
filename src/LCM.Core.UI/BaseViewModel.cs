using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LCM.Core.UI
{
    public class BaseViewModel : Screen
    {
        protected readonly IEventAggregator _evAggregator;

        public BaseViewModel(IEventAggregator eventAggregator)
        {
            this._evAggregator = eventAggregator;
            this.PropertyChanged += viewModel_PropertyChanged;
        }

        protected virtual void viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnActivate()
        {
            _evAggregator.Subscribe(this);
        }

        protected override void OnDeactivate(bool close)
        {
            _evAggregator.Unsubscribe(this);
        }
    }
}
