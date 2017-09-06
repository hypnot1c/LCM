using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;

namespace LCM.Core.UI.ViewModels.Cards
{
    public class CardCreateBaseViewModel : BaseViewModel
    {
        public CardCreateBaseViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            
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

        public virtual void Save()
        {

        }
    }
}
