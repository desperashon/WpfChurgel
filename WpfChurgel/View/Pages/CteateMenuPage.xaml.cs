using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfChurgel.Models; 

namespace WpfChurgel.View.Pages
{
    public partial class CteateMenuPage : Page
    {
        public CteateMenuPage()
        {
            InitializeComponent();
          

            
            MenuDatePicker.SelectedDate = DateTime.Today;

            
            DishesListBox.ItemsSource = App.context.Dishes.ToList();
            LoadMenuTypes();
        }

        private void LoadMenuTypes()
        {
            
            var menuTypes = App.context.TypeDishes.ToList();

           
            menuTypes.Insert(0, new TypeDishes { Name = "Все", id = 0 });

            
            MenuTypeComboBox.ItemsSource = menuTypes;

            
            MenuTypeComboBox.DisplayMemberPath = "Name";
            MenuTypeComboBox.SelectedValuePath = "id";

          
            MenuTypeComboBox.SelectedIndex = 0;
        }

        private void MenuTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedMenuType = MenuTypeComboBox.SelectedItem as TypeDishes;

            if (selectedMenuType != null)
            {
                if (selectedMenuType.id == 0)
                {
                    
                    DishesListBox.ItemsSource = App.context.Dishes.ToList();
                }
                else
                {
                    
                    var filteredDishes = App.context.Dishes.Where(d => d.IdTypeDishes == selectedMenuType.id).ToList();
                    DishesListBox.ItemsSource = filteredDishes;
                }
            }
        }



        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            
            var selectedDish = (DishesListBox.SelectedItem as Dishes);

            if (selectedDish != null)
            {
              
                DateTime? selectedDate = MenuDatePicker.SelectedDate;

                
                AddDishToMenu(selectedDish, selectedDate);

                
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите блюдо для добавления в меню.");
            }
        }

        private void AddDishToMenu(Dishes dish, DateTime? menuDate)
        {
            try
            {
                
                if (menuDate.HasValue)
                {
                   
                    var existingMenu = App.context.Menu.FirstOrDefault(m => m.Date == menuDate.Value);


                    if (existingMenu == null)
                    {
                        
                        existingMenu = new Models.Menu() { Date = menuDate.Value };
                        App.context.Menu.Add(existingMenu);
                        App.context.SaveChanges(); 
                    }

                 
                    existingMenu.Dishes.Add(dish);

                    
                    App.context.SaveChanges();

                    
                    MessageBox.Show($"Блюдо \"{dish.Name}\" успешно добавлено в меню на {menuDate.Value.ToShortDateString()}");
                }
                else
                {
                    MessageBox.Show("Не удалось добавить блюдо в меню: не выбрана дата меню.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении блюда в меню: {ex.Message}");
            }
        }

    }
}
