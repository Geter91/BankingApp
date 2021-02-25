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

namespace BankingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {

        static BankManager BankM;
        static BankView BankV;
        public MainWindow()
        {
            BankV = new BankView();
            BankM = new BankManager(BankV);

            BankV.BankM = BankM;
            InitializeComponent();
            this.DataContext = BankV;
        }

        void CreateTransactions(object sender, RoutedEventArgs e)
        {
            BankM.CreateTransactions();
        }

        void ProccessTransfers (object sender, RoutedEventArgs e)
        {
            BankM.ProccessTransfers();
        }


    }
}
