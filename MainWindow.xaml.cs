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
using System.IO; //для осуществления чтения/записи в файл
using System.Diagnostics; //для запуска Блокнота
using Microsoft.Win32; //для работы диалоговых окон открытия/сохранения файла

namespace WpfApp1
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

        private void MainFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            if (!(e.Content is Page page)) return;
            this.Title = $"Bulgakov - {page.Title}";

            if (page is Pages.AuthPage)
                ButtonBack.Visibility = Visibility.Hidden;
            else
                ButtonBack.Visibility = Visibility.Visible;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack) MainFrame.GoBack();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            MainFrame?.Navigate(new RegPage());
        }

        void Export_Click(object sender, RoutedEventArgs e)
        {
            string path = "export.txt";
            StreamWriter sw = new StreamWriter(path);

            using (var db = new EmployeesEntities())
            {
                string IDline = String.Join(":", db.Employees.Select(x => x.ID));
                sw.Write(":");
                sw.WriteLine(IDline);

                string Loginline = String.Join(":", db.Employees.Select(x => x.Login));
                sw.Write(":");
                sw.WriteLine(Loginline);

                string Passwordline = String.Join(":", db.Employees.Select(x => x.Password));
                sw.Write(":");
                sw.WriteLine(Passwordline);

                string Roleline = String.Join(":", db.Employees.Select(x => x.Role));
                sw.Write(":");
                sw.WriteLine(Roleline);

                string FIOline = String.Join(":", db.Employees.Select(x => x.FIO));
                sw.Write(":");
                sw.WriteLine(FIOline);

                sw.Close();

                Process.Start("notepad.exe", path);
            }

        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            if (ofd.FileName != "") // проверка на выбор файла
            {
                StreamReader sr = new StreamReader(ofd.FileName); // открываем файл
                while (!sr.EndOfStream) // перебираем строки, пока они не закончены
                {
                    string[] lines = new string[5];// массив данных 
                    for (int i = 0; i < 5; i++) // читаем 5 строк 
                    {
                        string line = sr.ReadLine(); // читаем строку  
                        string[] data = line.Split(':');
                        line = ""; // обнуляем переменную    
                        if (data.Length >= 2) // проверяем на целостность данных  
                        {
                            for (int j = 1; j < data.Length; j++) // складываем данные      
                            {
                                line += " ";
                                line += data[j]; // собираем строку  
                            }
                        }
                        lines[i] = line; // записываем значения в массив 
                    }
                    // выводим данные 
                    MessageBox.Show("Данные в файле: \nID: " + lines[0] + "\nФИО: " + lines[1] + "\nЛогин: " + lines[2] + "\nПароль: " + lines[3] + "\nРоль: " + lines[4]);
                }
            }
            else MessageBox.Show("Файл для импорта не выбран!");
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            MainFrame?.Navigate(new AdminMenu());
        }
    }
}
