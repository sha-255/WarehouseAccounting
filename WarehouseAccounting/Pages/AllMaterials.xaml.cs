using System.Windows.Controls;
using System.Windows.Navigation;

namespace WarehouseAccounting.Pages
{
    /// <summary>
    /// Interaction logic for AllMaterials.xaml
    /// </summary>
    public partial class AllMaterials : Page
    {
        public AllMaterials()
        {
            InitializeComponent();

            NMain.Click += (s, e) => NavigationService.Navigate(new Main());
            NMFF.Click += (s, e) => NavigationService.Navigate(new MaterialsForTheManufactureOfFurniture());
            NMFTC.Click += (s, e) => NavigationService.Navigate(new MaterialsForTheCountertop());
        }

        private void StackPanel_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
