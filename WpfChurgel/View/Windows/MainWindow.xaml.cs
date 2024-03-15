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
using WpfChurgel.View.Pages;

namespace WpfChurgel
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateMenu_Click(object sender, RoutedEventArgs e)
        {
        
            NavigationFrame.Navigate(new CteateMenuPage());
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            NavigationFrame.Navigate(new MenuPage());
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationFrame.Navigate(new DishesPage());
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationFrame.Navigate(new OrderPage());
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            NavigationFrame.Navigate(new ProductPage());
        }
    }
}
