using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HitungCepat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RankingPage : ContentPage
    {
        private Controllers.PemainController _pemainController = new Controllers.PemainController();

        public RankingPage()
        {
            InitializeComponent();
            btnHome.Clicked += async (sender, e) => { await Navigation.PopModalAsync(); };
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await RefreshItemAsync(true);
        }

        private async Task RefreshItemAsync(bool showActivityIndicator)
        {
            using (var scope = new Controllers.ActivityIndicatorScope(syncIndicator, showActivityIndicator))
            {
                listPemain.ItemsSource = await _pemainController.GetPemainAsync();
            }
        }

        private async void ListPemain_Refreshing(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            Exception error = null;

            try
            {
                await RefreshItemAsync(false);
            }
            catch (Exception ex)
            {
                error = ex;
            }
            finally
            {
                list.EndRefresh();
            }

            if (error != null)
            {
                await DisplayAlert("Refresh error!", "Couldn't refresh data (" + error.Message + ")", "OK");
            }
        }
    }
}