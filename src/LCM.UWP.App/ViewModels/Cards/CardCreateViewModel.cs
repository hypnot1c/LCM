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
        //this.ImageBitmap = new BitmapImage();
        //this.ImageBitmap.SetSource(stream);
        
        // Application now has read/write access to the picked file
        //OutputTextBlock.Text = "Picked photo: " + file.Name;
      }
      else
      {
        //OutputTextBlock.Text = "Operation cancelled.";
      }
    }

    private CaptureElement captureElement;

    public CaptureElement CaptureElement
    {
      get { return captureElement; }
      set { captureElement = value; base.NotifyOfPropertyChange(() => this.CaptureElement); }
    }

    private MediaCapture mediaCapture;

    public MediaCapture MediaCapture
    {
      get { return mediaCapture; }
      set { mediaCapture = value; base.NotifyOfPropertyChange(() => this.MediaCapture); }
    }

    public async Task TakePicture()
    {
      this.CaptureElement = new CaptureElement();
      var cameraDevices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

      var backFacingDevice = cameraDevices
                    .FirstOrDefault(c => c.EnclosureLocation?.Panel == Windows.Devices.Enumeration.Panel.Back);

      var preferredDevice = backFacingDevice ?? cameraDevices.FirstOrDefault();

      // Create MediaCapture
      this.MediaCapture = new MediaCapture();

      // Stop the screen from timing out.
      //_displayRequest.RequestActive();

      // Initialize MediaCapture and settings
      try
      {
        await this.MediaCapture.InitializeAsync(new MediaCaptureInitializationSettings
        {
          VideoDeviceId = preferredDevice.Id
        });

        this.CaptureElement.Source = this.MediaCapture;

        await MediaCapture.StartPreviewAsync();

        await SetPreviewRotationPropertiesAsync();
      }
      catch (Exception ex)
      {
        Debug.WriteLine("The app was denied access to the camera");
      }

      // Set the preview source for the CaptureElement
      //PreviewControl.Source = _mediaCapture;

      // Start viewing through the CaptureElement 

      // Set rotation properties to ensure the screen is filled with the preview.

    }

    private static readonly Guid RotationKey = new Guid("C380465D-2271-428C-9B83-ECEA3B4A85C1");

    private async Task SetPreviewRotationPropertiesAsync()
    {
      // Only need to update the orientation if the camera is mounted on the device
      // if (_externalCamera) return;

      // Calculate which way and how far to rotate the preview
      int rotation = ConvertDisplayOrientationToDegrees(DisplayInformation.GetForCurrentView().CurrentOrientation);

      // Get the property meta data about the video.
      var props = MediaCapture.VideoDeviceController.GetMediaStreamProperties(MediaStreamType.VideoPreview);

      // Change the meta data to rotate the preview to fill the screen with the preview.
      props.Properties.Add(RotationKey, rotation);

      // Now set the updated meta data into the video preview.
      await MediaCapture.SetEncodingPropertiesAsync(MediaStreamType.VideoPreview, props, null);
    }

    private static int ConvertDisplayOrientationToDegrees(DisplayOrientations orientation)
    {
      switch (orientation)
      {
        case DisplayOrientations.Portrait:
          return 90;
        case DisplayOrientations.LandscapeFlipped:
          return 180;
        case DisplayOrientations.PortraitFlipped:
          return 270;
        case DisplayOrientations.Landscape:
        default:
          return 0;
      }
    }
  }
}
