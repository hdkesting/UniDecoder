using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UniDecoderWpf.Views
{
    public sealed partial class SettingsPage : Page
    {
        Template10.Services.SerializationService.ISerializationService _SerializationService;

        public SettingsPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
            this._SerializationService = Template10.Services.SerializationService.SerializationService.Json;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var index = int.Parse(this._SerializationService.Deserialize(e.Parameter?.ToString()).ToString());
            this.MyPivot.SelectedIndex = index;
        }
    }
}