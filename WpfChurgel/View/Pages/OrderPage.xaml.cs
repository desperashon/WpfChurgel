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
        private decimal totalCost;
        List<Category> categories = new List<Category>();
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

        private void CreateCheque_Click(object sender, RoutedEventArgs e)
        {
           
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
        
                foreach (var product in PositionsLv.Items)
                {
                    Products selectedProduct = product as Products;
                    if (selectedProduct != null)
                    {
                        int dishId = selectedProduct.ProductDishRelation.FirstOrDefault()?.DishID ?? 0;

                        DishOrderRelation relation = new DishOrderRelation
                        {
                            DishID = dishId,
                            OrderID = newOrder.ID,
                            Portions = 1
                        };

                        App.context.DishOrderRelation.Add(relation);
                    }
                }

                
                App.context.SaveChanges();

               
                PositionsLv.Items.Clear();

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

        private void CategoryCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryCmb.SelectedIndex != 0)
            {
                PositionLsb.ItemsSource = App.context.Products.Where(p => p.Category.IdCategory == CategoryCmb.SelectedIndex).ToList();
            }
            else
            {
                PositionLsb.ItemsSource = App.context.Products.ToList();
            }
        }

        private void PositionLsb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.selectedPosition = PositionLsb.SelectedItem as Products;

            if (App.selectedPosition != null)
            {
                PositionsLv.Items.Add(App.selectedPosition);

                TotalCostTbl.Text = GetTotalCost();
            }

            
            PositionLsb.SelectedIndex = -1;
        }

        private string GetTotalCost()
        {
            totalCost = 0;
            foreach (Products position in PositionsLv.Items)
            {
                totalCost += position.Price ?? 0; 
            }
            return $"К оплате: {totalCost}₽";
        }

        private void PositionsLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PositionsLv.Items.Remove(PositionsLv.SelectedItem);

            TotalCostTbl.Text = GetTotalCost();
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            
            DateTbl.Text = "" + DateTime.Now.ToString();

            
            PositionLsb.ItemsSource = App.context.Products.ToList();

            
            categories = App.context.Category.ToList();
            categories.Insert(0, new Category() { Name = "Все категории" });
            CategoryCmb.ItemsSource = categories;

           
            CategoryCmb.SelectedIndex = 0;
        }

    }
}
