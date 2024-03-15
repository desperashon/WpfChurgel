using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfChurgel.Models;

namespace WpfChurgel.View.Pages
{
    public partial class OrderPage : Page, INotifyPropertyChanged
    {
        private DateTime _selectedMenuDate;
        public DateTime SelectedMenuDate
        {
            get { return _selectedMenuDate; }
            set
            {
                if (_selectedMenuDate != value)
                {
                    _selectedMenuDate = value;
                    OnPropertyChanged(nameof(SelectedMenuDate));
                }
            }
        }

        private List<Products> _availableProducts;
        public List<Products> AvailableProducts
        {
            get { return _availableProducts; }
            set
            {
                if (_availableProducts != value)
                {
                    _availableProducts = value;
                    OnPropertyChanged(nameof(AvailableProducts));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public OrderPage()
        {
            InitializeComponent();
            DataContext = this;
            LoadAvailableProducts(DateTime.Today);
        }

        private void LoadAvailableProducts(DateTime menuDate)
        {
            var menuForDate = App.context.Menu.FirstOrDefault(m => m.Date == menuDate);

            if (menuForDate != null)
            {
                SelectedMenuDate = menuForDate.Date ?? DateTime.MinValue;
                var menuDishes = menuForDate.Dishes.ToList();

                List<Products> availableProducts = new List<Products>();
                foreach (var dish in menuDishes)
                {
                    foreach (var relation in dish.ProductDishRelation)
                    {
                        availableProducts.Add(relation.Products);
                    }
                }

                AvailableProducts = availableProducts;
            }
            else
            {
                AvailableProducts = new List<Products>();
            }
        }

        private void MenuDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MenuDatePicker.SelectedDate.HasValue)
            {
                LoadAvailableProducts(MenuDatePicker.SelectedDate.Value);
            }
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (AvailableProducts.Count == 0)
            {
                MessageBox.Show("Нет доступных продуктов для заказа.");
                return;
            }


            if (IsOrderExistsForSelectedDate())
            {
                MessageBox.Show("На выбранную дату уже создан заказ.");
                return;
            }

           
            Orders newOrder = new Orders
            {
                OrderDate = DateTime.Now, 
                UserId = 1,            
            };

            
            App.context.Orders.Add(newOrder);

            try
            {
                
                foreach (var product in AvailableProducts)
                {  
                    int dishId = product.ProductDishRelation.FirstOrDefault()?.DishID ?? 0;

                   
                    DishOrderRelation relation = new DishOrderRelation
                    {
                        DishID = dishId, 
                        OrderID = newOrder.ID, 
                        Portions = 1 
                    };

               
                    App.context.DishOrderRelation.Add(relation);
                }

                
                App.context.SaveChanges();

                
                AvailableProducts.Clear();

                MessageBox.Show("Заказ успешно создан.");

              
                LoadAvailableProducts(SelectedMenuDate);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"Произошла ошибка при создании заказа: {ex.Message}");
            }
        }

        private bool IsOrderExistsForSelectedDate()
{
   
    return App.context.Orders.Any(o => o.OrderDate == SelectedMenuDate.Date);
}






    }
}
