using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using WpfChurgel.Models; 

namespace WpfChurgel.View.Pages
{
    public partial class DishesPage : Page, INotifyPropertyChanged
    {
        private decimal _totalPrice;
        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                if (_totalPrice != value)
                {
                    _totalPrice = value;
                    OnPropertyChanged("TotalPrice");
                }
            }
        }

        public DishesPage()
        {
            InitializeComponent();
            DataContext = this;
           
            DishesListBox.ItemsSource = App.context.Dishes.ToList();
        }

        
        private void DishesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Dishes selectedDish = (sender as ListBox).SelectedItem as Dishes;

            
            if (selectedDish != null)
            {
                
                var productIds = App.context.ProductDishRelation
                    .Where(dp => dp.DishID == selectedDish.ID)
                    .Select(dp => dp.ProductID)
                    .ToList();

               
                var products = App.context.Products.Where(p => productIds.Contains(p.ID)).ToList();

               
                TotalPrice = (decimal)products.Sum(p => p.Price);

                DishProductsDataGrid.ItemsSource = products;
            }
            else
            {
                
                DishProductsDataGrid.ItemsSource = null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
