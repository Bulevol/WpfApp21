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
using static WpfApp1.EmployeesEntities;
using static WpfApp1.Employees;


namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private WpfApp1.Employees _currentEmployee = new WpfApp1.Employees();
        
        public AddEditPage(WpfApp1.Employees selectedEmployee)
        {
            InitializeComponent();
            
            if (selectedEmployee != null)
                _currentEmployee = selectedEmployee;

            DataContext = _currentEmployee;

            CmbCat.ItemsSource = EmployeesEntities.GetContext().Employees.ToList();
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {

            StringBuilder errors = new StringBuilder();
            
            if (_currentEmployee.ID == null)
                errors.AppendLine("Выберите ID сотрудника!");

            if (string.IsNullOrWhiteSpace(_currentEmployee.Login))
                errors.AppendLine("Укажите логин сотрудника!");

            if (string.IsNullOrWhiteSpace((_currentEmployee.Password)))
                errors.AppendLine("Укажите пароль!");
            
            if (string.IsNullOrWhiteSpace(_currentEmployee.Role))
                errors.AppendLine("Укажите роль сотрудника!");

            if (string.IsNullOrWhiteSpace(_currentEmployee.FIO))
                errors.AppendLine("Укажите ФИО сотрудника");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentEmployee.ID == 0)
                EmployeesEntities.GetContext().Employees.Add(_currentEmployee);

            try
            {
                EmployeesEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
    }
}
