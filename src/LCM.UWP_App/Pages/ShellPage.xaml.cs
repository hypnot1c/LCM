﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LCM.UWP_App.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellPage : Page
    {
        public ShellPage()
        {
            this.InitializeComponent();
        }

        public void SetContentFrame(Frame frame)
        {
            //rootSplitView.Content = frame;
        }

        public void SetMenuPaneContent(UIElement content)
        {
            //rootSplitView.Pane = content;
        }
    }
}
