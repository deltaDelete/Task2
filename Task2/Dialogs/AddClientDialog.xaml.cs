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
using System.Windows.Shapes;

using Task2.Database;

namespace Task2.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для AddUserDialog.xaml
    /// </summary>
    public partial class AddClientDialog : Window
    {
        public AddClientDialog()
        {
            Owner = Application.Current.MainWindow;
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrEmpty(EmailTextBox.Text) || !String.IsNullOrEmpty(PhoneTextBox.Text))
            {
                Client client = new Client()
                {
                    Email = EmailTextBox.Text,
                    Phone = PhoneTextBox.Text,
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    MiddleName = MidNameTextBox.Text
                };
                using (var db = new ApplicationContext())
                {
                    db.Clients.Attach(client);
                    db.Clients.Add(client);
                    db.SaveChanges();
                    ((MainWindow)Application.Current.MainWindow).Clients = db.Clients.ToList();
                }
                Close();
            }
        }
    }
}
