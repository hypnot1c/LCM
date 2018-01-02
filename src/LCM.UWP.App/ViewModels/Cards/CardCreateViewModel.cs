using LCM.Core.UI.ViewModels.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using LCM.Core.Service;
using LCM.Core.Entity.Cards;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using System.IO;
using Windows.Devices.Enumeration;
using Windows.Media.Capture;
using Windows.Graphics.Display;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;

namespace LCM.UWP.App.ViewModels.Cards
{
  public class CardCreateViewModel : CardCreateBaseViewModel
  {
    public CardCreateViewModel(
        IEventAggregator eventAggregator,
        INavigationService navigationService,
        LCMUnitOfWork unitOfWork
        ) : base(eventAggregator, unitOfWork)
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
        this._navService.For<CardListViewModel>().Navigate();
      }
    }

    public void Cancel()
    {
      if (this._navService.CanGoBack)
      {
        this._navService.GoBack();
      }
      else
      {
        this._navService.For<CardListViewModel>().Navigate();
      }
    }

    private BitmapImage _imageBitmap;

    public BitmapImage ImageBitmap
    {
      get { return _imageBitmap; }
      set { _imageBitmap = value; base.NotifyOfPropertyChange(() => this.ImageBitmap); }
    }

    public void OnCardContextMenu(object sender, RightTappedRoutedEventArgs e)
    {
      var senderElement = sender as FrameworkElement;
      // If you need the clicked element:
      // Item whichOne = senderElement.DataContext as Item;
      var flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);
      flyoutBase.ShowAt(senderElement);
    }

    public async void SelectImage()
    {
      var openPicker = new FileOpenPicker();
      openPicker.ViewMode = PickerViewMode.Thumbnail;
      openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
      openPicker.FileTypeFilter.Add(".jpg");
      openPicker.FileTypeFilter.Add(".jpeg");
      openPicker.FileTypeFilter.Add(".png");

      var file = await openPicker.PickSingleFileAsync();
      if (file != null)
      {
        using (var inputStream = file.OpenSequentialReadAsync().AsTask().Result)
        {
          var readStream = inputStream.AsStreamForRead();

          var byteArray = new byte[readStream.Length];
          var r = readStream.ReadAsync(byteArray, 0, byteArray.Length).Result;
          this.Image = byteArray;
        }
      }
      else
      {
        //OutputTextBlock.Text = "Operation cancelled.";
      }
    }

    public async Task TakePicture()
    {
      var captureUI = new CameraCaptureUI();
      captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
      captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

      var photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

      if (photo == null)
      {
        // User cancelled photo capture
        return;
      }

      var stream = await photo.OpenAsync(FileAccessMode.Read);
      var readStream = stream.AsStreamForRead();
      var byteArray = new byte[readStream.Length];
      var r = readStream.ReadAsync(byteArray, 0, byteArray.Length).Result;
      this.Image = byteArray;
      //var decoder = await BitmapDecoder.CreateAsync(stream);
      //var softwareBitmap = await decoder.GetSoftwareBitmapAsync();
    }
  }
}
