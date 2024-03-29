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
using WpfApp1.Pages;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для Employees.xaml
    /// </summary>
    public partial class Employees : Page
    {
        public Employees()
        {
            InitializeComponent();

            DataGridEmployee.ItemsSource = EmployeesEntities.GetContext().Employees.ToList();
        }

        private void DataGridEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            EmployeesFrame?.Navigate(new AddEditPage((sender as Button).DataContext as WpfApp1.Employees));
        }

        private void ButtonDel_OnClick(object sender, RoutedEventArgs e)
        {
            var employeesForRemoving = DataGridEmployee.SelectedItems.Cast<WpfApp1.Employees>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {employeesForRemoving.Count()} элементов?", "Внимание",
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    EmployeesEntities.GetContext().Employees.RemoveRange(employeesForRemoving);
                    EmployeesEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    DataGridEmployee.ItemsSource = EmployeesEntities.GetContext().Employees.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            EmployeesFrame?.Navigate(new AddEditPage(null));
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                EmployeesEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                DataGridEmployee.ItemsSource = EmployeesEntities.GetContext().Employees.ToList();
            }
        }
    }
}
