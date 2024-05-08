using System.Windows;
using WarehouseAccounting.Common;
using WarehouseAccounting.Pages;

namespace WarehouseAccounting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Initialized += (s, e) => Settings.Apply();
            InitializeComponent();
            MainFrame.Navigate(new Main());
        }
    }
}