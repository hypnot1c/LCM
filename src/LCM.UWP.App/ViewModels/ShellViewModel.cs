using Caliburn.Micro;
using LCM.Data.Master.EF;
using LCM.Data.Master.Model;
using LCM.UWP.App.Messages;
using LCM.UWP.App.Resources;
using LCM.UWP.App.ViewModels.Cards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace LCM.UWP.App.ViewModels
{
  public class ShellViewModel: Screen, IHandle<ResumeStateMessage>, IHandle<SuspendStateMessage>
  {
    public ShellViewModel(WinRTContainer container, IEventAggregator eventAggregator)
    {
      //this.PropertyChanged += ShellViewModel_PropertyChanged;
      this._container = container;
      this._eventAggregator = eventAggregator;
      this.CollapsedPanelLength = 0;

      this.NavMenuItems = new List<NavMenuItem>
      {
        new NavMenuItem { Icon = Symbol.Home, Title = "Home", TargetViewModel = typeof(CardListViewModel) }
      };
      this._selectedNavMenuItem = this.NavMenuItems.First();
      // Register a handler for BackRequested events and set the
      // visibility of the Back button
      SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
    }

    private readonly WinRTContainer _container;
    private readonly IEventAggregator _eventAggregator;
    protected INavigationService _navigationService;
    private bool _resume;

    private int _collapsedPanelLength;
    public int CollapsedPanelLength
    {
      get { return _collapsedPanelLength; }
      set
      {
        _collapsedPanelLength = value;
        this.NotifyOfPropertyChange(() => this.CollapsedPanelLength);
      }
    }

    public IEnumerable<NavMenuItem> NavMenuItems { get; set; }

    private NavMenuItem _selectedNavMenuItem;

    public NavMenuItem SelectedNavMenuItem
    {
      get { return this._selectedNavMenuItem; }
      set
      {
        this._selectedNavMenuItem = value;
        this.NotifyOfPropertyChange(() => this.SelectedNavMenuItem);
      }
    }

    protected override void OnActivate()
    {
      _eventAggregator.Subscribe(this);
    }

    protected override void OnDeactivate(bool close)
    {
      _eventAggregator.Unsubscribe(this);
    }

    public void Handle(ResumeStateMessage message)
    {
      throw new NotImplementedException();
    }

    public void Handle(SuspendStateMessage message)
    {
      this._navigationService.SuspendState();
    }

    private void OnBackRequested(object sender, BackRequestedEventArgs e)
    {
      if (this._navigationService.CanGoBack && !e.Handled)
      {
        e.Handled = true;
        this._navigationService.GoBack();
      }
    }

    public void SetupNavigationService(Frame frame)
    {
      if (_container.HasHandler(typeof(INavigationService), null))
      {
        _container.UnregisterHandler(typeof(INavigationService), null);
      }

      this._navigationService = _container.RegisterNavigationService(frame);

      this._navigationService.Navigated += _navigationService_Navigated;
      this._navigationService.NavigationFailed += _navigationService_NavigationFailed;

      this.NavigateTo();
    }

    private void _navigationService_Navigated(object sender, NavigationEventArgs e)
    {
      SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
            ((Frame)sender).CanGoBack ?
            AppViewBackButtonVisibility.Visible :
            AppViewBackButtonVisibility.Collapsed;

      this.CollapsedPanelLength = 0;
    }

    private void _navigationService_NavigationFailed(object sender, NavigationFailedEventArgs e)
    {
      throw new System.NotImplementedException();
    }

    public void NavigateTo()
    {
      if (_resume)
      {
        this._navigationService.ResumeState();
      }
      else
      {
        this._navigationService.NavigateToViewModel(this.SelectedNavMenuItem.TargetViewModel);
      }
    }

  }
}
