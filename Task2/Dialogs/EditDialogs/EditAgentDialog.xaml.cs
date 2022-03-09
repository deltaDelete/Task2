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
    public partial class EditAgentDialog : Window
    {
        private Agent EditableAgent;
        public EditAgentDialog(Agent agent)
        {
            Owner = Application.Current.MainWindow;
            InitializeComponent();
            EditableAgent = agent;
            DealShareTextBox.Text = EditableAgent.DealShare.ToString();
            FirstNameTextBox.Text = EditableAgent.FirstName;
            LastNameTextBox.Text = EditableAgent.LastName;
            MidNameTextBox.Text = EditableAgent.MiddleName;

        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
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
            using (var db = new ApplicationContext())
            {
                var agent = db.Agents.First(x => x.Id == EditableAgent.Id);
                if (agent != null)
                {
                    agent.DealShare = dealshare;
                    agent.FirstName = FirstNameTextBox.Text;
                    agent.LastName = LastNameTextBox.Text;
                    agent.MiddleName = MidNameTextBox.Text;
                    db.Agents.Update(agent);
                }
                else MessageBox.Show("Не удалось получить доступ к записи");
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
