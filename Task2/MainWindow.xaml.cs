using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Task2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private List<Client> clientsValue = new();
        private List<Agent> agentsValue = new();

        public MainWindow()
        {
            InitializeComponent();
            ClientsGrid.DataContext = this;
            AgentsGrid.DataContext = this;
            using (ApplicationContext context = new ApplicationContext())
            {
                Clients = context.Clients.ToList();
                Agents = context.Agents.ToList();
            }
        }
        public void ClientsAddClick(object sender, RoutedEventArgs e)
        {

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #region INotifyPropertyChanged Members
        public List<Database.Client> Clients
        {
            set
            {
                clientsValue = value;
                NotifyPropertyChanged(nameof(Clients));

            }
            get
            {
                return clientsValue;
            }
        }
        public List<Agent> Agents
        {
            set
            {
                agentsValue = value;
                NotifyPropertyChanged(nameof(Agents));

            }
            get
            {
                return agentsValue;
            }
        }
        #endregion
    }
}
