using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfChurgel.Models; 

namespace WpfChurgel.View.Windows
{
    public partial class AddProductBtnWindow : Window
    {
        public AddProductBtnWindow()
        {
            InitializeComponent();
            UnitComboBox.ItemsSource =  App.context.UnitId.ToList();
            UnitComboBox.DisplayMemberPath = "Name";
            UnitComboBox.SelectedValuePath = "UnitId";
           
            CategoryComboBox.ItemsSource = App.context.Category.ToList();
            CategoryComboBox.DisplayMemberPath = "Name";
            CategoryComboBox.SelectedValuePath = "IdCategory"; 
        }

        
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
          
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                CategoryComboBox.SelectedItem == null ||
                UnitComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(ProteinTextBox.Text) ||
                string.IsNullOrWhiteSpace(FatTextBox.Text) ||
                string.IsNullOrWhiteSpace(CarbohydratesTextBox.Text) ||
                string.IsNullOrWhiteSpace(PriceTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

           
            Products newProduct = new Products
            {
                Name = NameTextBox.Text,
                Category = CategoryComboBox.SelectedItem as Category,
                UnitId1 = UnitComboBox.SelectedItem as UnitId,
                Protein = decimal.Parse(ProteinTextBox.Text),
                Fat = decimal.Parse(FatTextBox.Text),
                Acne = decimal.Parse(CarbohydratesTextBox.Text),
                Price = decimal.Parse(PriceTextBox.Text)
            };

        
            App.context.Products.Add(newProduct);
            App.context.SaveChanges();

            
            Close();
        }


     
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
          
            Close();
        }
        



    }
}
