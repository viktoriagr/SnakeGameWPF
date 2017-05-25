using System.Windows;
using System.Windows.Controls;

namespace WpfSnakeGameVGr.Views
{
    /// <summary>
    /// Interaction logic for InstrctuctionsView.xaml
    /// </summary>
    public partial class InstrctuctionsView : UserControl
    {
        private object oldPresenterContent;
        private MainWindow parentWindow;

        public InstrctuctionsView()
        {
            InitializeComponent();
            parentWindow = Application.Current.MainWindow as MainWindow;
            if (parentWindow != null)
            {
                this.oldPresenterContent = parentWindow.viewPresenter.Content;
            }
        }

        private void GoBackClick(object sender, RoutedEventArgs e)
        {
            if (oldPresenterContent != null)
            {
                parentWindow.viewPresenter.Content = this.oldPresenterContent;
            }
        }
    }
}
