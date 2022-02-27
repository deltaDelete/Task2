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
    /// Логика взаимодействия для AddAgentDialog.xaml
    /// </summary>
    public partial class AddAgentDialog : Window
    {
        public AddAgentDialog()
        {
            InitializeComponent();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            int dealshare = 0;
            try
            {
                dealshare = Int32.Parse(DealShareTextBox.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Поле \"Доля от покупки\" принимает значение только от 0 до 100");
                return;
            }
            if (String.IsNullOrEmpty(FirstNameTextBox.Text))
            {
                MessageBox.Show("Поле \"Имя\" обязательно к заполнению");
                return;
            }
            if (String.IsNullOrEmpty(LastNameTextBox.Text))
            {
                MessageBox.Show("Поле \"Фамилия\" обязательно к заполнению");
                return;
            }
            if (String.IsNullOrEmpty(MidNameTextBox.Text))
            {
                MessageBox.Show("Поле \"Отчество\" обязательно к заполнению");
                return;
            }
            Agent agent = new Agent()
            {
                FirstName = FirstNameTextBox.Text,
                LastName = LastNameTextBox.Text,
                MiddleName = MidNameTextBox.Text,
                DealShare = dealshare
            };
            using (var db = new ApplicationContext())
            {
                db.Agents.Attach(agent);
                db.Agents.Add(agent);
                db.SaveChanges();
                ((MainWindow)Application.Current.MainWindow).Agents = db.Agents.ToList();
            }
            Close();
        }

        private void DealShareTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                int dealshare = Int32.Parse(e.Text);
                e.Handled = dealshare >= 0 && dealshare <= 100;
            }
            catch (Exception)
            {
                e.Handled = false;
            }
        }
    }
}
