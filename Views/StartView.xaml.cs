using System.Windows;
using System.Windows.Controls;

namespace WpfSnakeGameVGr.Views
{
    /// <summary>
    /// Interaction logic for StartView.xaml
    /// </summary>
    public partial class StartView : UserControl
    {
        private MainWindow parentWindow;

        public StartView()
        {
            InitializeComponent();

            this.parentWindow = Application.Current.MainWindow as MainWindow;
        }

        private void NewGameClick(object sender, RoutedEventArgs e)
        {
            this.parentWindow.viewPresenter.Content = new SnakeSpeedView();
        }

        private void InstructionsClick(object sender, RoutedEventArgs e)
        {
            this.parentWindow.viewPresenter.Content = new InstrctuctionsView();
        }
    }
}
