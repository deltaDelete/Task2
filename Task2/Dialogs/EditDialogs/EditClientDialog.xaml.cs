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
    public partial class EditClientDialog : Window
    {
        private Client EditableClient;
        public EditClientDialog(Client client)
        {
            Owner = Application.Current.MainWindow;
            InitializeComponent();
            EditableClient = client;
            LastNameTextBox.Text = EditableClient.LastName;
            FirstNameTextBox.Text = EditableClient.FirstName;
            MidNameTextBox.Text = EditableClient.MiddleName;
            EmailTextBox.Text = EditableClient.Email;
            PhoneTextBox.Text = EditableClient.Phone;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrEmpty(EmailTextBox.Text) || !String.IsNullOrEmpty(PhoneTextBox.Text))
            {
                using (var db = new ApplicationContext())
                {
                    var client = db.Clients.First(x => x.Id == EditableClient.Id);
                    if (client != null)
                    {
                        client.LastName = LastNameTextBox.Text;
                        client.FirstName = FirstNameTextBox.Text;
                        client.MiddleName = MidNameTextBox.Text;
                        client.Email = EmailTextBox.Text;
                        client.Phone = PhoneTextBox.Text;
                        db.Clients.Update(client);
                    }
                    else MessageBox.Show("Не удалось получить доступ к записи");
                    db.SaveChanges();
                    ((MainWindow)Application.Current.MainWindow).Clients = db.Clients.ToList();
                }
                Close();
            }
        }
    }
}
