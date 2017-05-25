using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfSnakeGameVGr.Views
{
    public partial class SnakeSpeedView : UserControl
    {
        private TimeSpan FAST = new TimeSpan(9000);
        private TimeSpan MODERATE = new TimeSpan(10000);
        private TimeSpan SLOW = new TimeSpan(50000);

        public SnakeSpeedView()
        {
            InitializeComponent();
        }

        private void StartGameClick(object sender, RoutedEventArgs e)
        {
            var parentWindow = Application.Current.MainWindow as MainWindow;
            if (parentWindow != null)
            {
                if (this.slowButton.IsChecked == true)
                {
                    parentWindow.viewPresenter.Content = new GameView(this.SLOW);
                }
                else if (this.moderateButton.IsChecked == true)
                {
                    parentWindow.viewPresenter.Content = new GameView(this.MODERATE);

                }
                else
                {
                    parentWindow.viewPresenter.Content = new GameView(this.FAST);
                }
            }
        }
    }
}
