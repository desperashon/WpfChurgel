using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfChurgel.Models;
using WpfChurgel.View.Windows;

namespace WpfChurgel.View.Pages
{
    public partial class ProductPage : Page
    {
        public ProductPage()
        {
            InitializeComponent();

            
            var allCategories = App.context.Category.ToList();

          
            allCategories.Insert(0, new Category() { IdCategory = 0, Name = "Все" });

       
            CategoryComboBox.DisplayMemberPath = "Name";
            CategoryComboBox.SelectedValuePath = "IdCategory";
            CategoryComboBox.ItemsSource = allCategories;

           
            CategoryComboBox.SelectionChanged += CategoryComboBox_SelectionChanged;

           
            LoadProducts();
        }

  
        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        
            LoadProducts();
        }

      
        private void LoadProducts()
        {
            
            if (CategoryComboBox.SelectedItem != null)
            {
                
                int selectedCategoryId = (int)CategoryComboBox.SelectedValue;

                var products = (selectedCategoryId == 0) ? App.context.Products.ToList() : App.context.Products.Where(p => p.CategoryId == selectedCategoryId).ToList();

                ProductDataGrid.ItemsSource = products;
            }
            else
            {
               
                ProductDataGrid.ItemsSource = App.context.Products.ToList();
            }
        }

        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            
            Products selectedProduct = ProductDataGrid.SelectedItem as Products;

            if (selectedProduct != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этот продукт?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    
                    App.context.Products.Remove(selectedProduct);
                    App.context.SaveChanges();

                   
                    LoadProducts();
                }
            }
            else
            {
                MessageBox.Show("Выберите продукт для удаления.");
            }
        }

     
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
       
            AddProductBtnWindow addProductWindow = new AddProductBtnWindow();
            addProductWindow.Closed += AddProductWindow_Closed; 
            addProductWindow.ShowDialog();
        }

       
        private void AddProductWindow_Closed(object sender, EventArgs e)
        {
            
            LoadProducts();
        }

    }
}
