using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ProductsPage productsPage = new ProductsPage();
            MainFrame.Content = productsPage;

        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.GoBack();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                TitlePage.Text = "Страница списка цехов";
                backBtn.Visibility = Visibility.Visible;
                procedureBtn.Visibility = Visibility.Hidden;
            } else
            {
                TitlePage.Text = "Главная страница";
                backBtn.Visibility = Visibility.Hidden;
                procedureBtn.Visibility = Visibility.Visible;
            }
        }

        private void procedureBtn_Click(object sender, RoutedEventArgs e)
        {
            CalculationWindow calculationWindow = new CalculationWindow();
            calculationWindow.Show();
        }
    }
}
