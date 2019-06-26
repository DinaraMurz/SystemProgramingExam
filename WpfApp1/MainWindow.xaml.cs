using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public int number = 999999;
        public int[] numbers = new int[number];
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ReadyButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int n = int.Parse(textBox.Text);
                Mass mass = new Mass()
                {
                    Numbers = numbers,
                    N = n
                };

                ThreadPool.QueueUserWorkItem(AddToMass, mass);
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корекные данные");
            }
        }

        static public void AddToMass(object obj)
        {
            Mass mass = obj as Mass;
            mass.Numbers[mass.Numbers.Last()] = mass.N;
        }

        private void DownloadToDatabaseClick(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(DownloadToDatabase);
        }

        public async void DownloadToDatabase(object obj)
        {
            await Task.Run(() => {
                using (var context = new Context())
                {
                    context.Users.Add(new User()
                    {
                        FullName = nameTextBox.Text,
                        Password = ageTextBox.Text
                    });
                    context.SaveChanges();
                }
            });
        }
    }
}
