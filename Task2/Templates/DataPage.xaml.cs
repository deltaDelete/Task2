using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

using Task2.Database;

namespace Task2.Templates
{
    /// <summary>
    /// Логика взаимодействия для DataPage.xaml
    /// </summary>
    public partial class DataPage : UserControl
    {
        public DataPage()
        {
            InitializeComponent();
            DataContext = this;
        }
        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(object), typeof(DataPage), new PropertyMetadata(null));

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainGrid.ItemsSource is List<Client>)
                new Dialogs.AddClientDialog().Show();
            if (MainGrid.ItemsSource is List<Agent>)
                new Dialogs.AddAgentDialog().Show();
        }
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainGrid.SelectedItem == null) return;

            if (MainGrid.ItemsSource is List<Client>)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Client? entity = db.Clients.Where(x => x.Id == ((Client)MainGrid.SelectedItem).Id).First();
                    if (entity != null)
                        db.Clients.Remove(entity);
                    db.SaveChanges();

                    ((MainWindow)Application.Current.MainWindow).Clients = db.Clients.ToList();
                }
            }
            if (MainGrid.ItemsSource is List<Agent>)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Agent? entity = db.Agents.Where(x => x.Id == ((Agent)MainGrid.SelectedItem).Id).First();
                    if (entity != null)
                        db.Agents.Remove(entity);
                    db.SaveChanges();

                    ((MainWindow)Application.Current.MainWindow).Agents = db.Agents.ToList();
                }
            }
            MessageBox.Show("Удаление прошло успешно");
        }
    }
}
