using System.Windows;
using WpfSnakeGameVGr.Views;

namespace WpfSnakeGameVGr
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.viewPresenter.Content = new StartView();
        }

        private void NewGameClick(object sender, RoutedEventArgs e)
        {
            this.viewPresenter.Content = new SnakeSpeedView();
        }

        private void ExitGameClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void InstructionsClick(object sender, RoutedEventArgs e)
        {
            this.viewPresenter.Content = new InstrctuctionsView();
        }

        private void ClosingMainWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
