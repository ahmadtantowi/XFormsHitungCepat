using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HitungCepat.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResultPage : ContentPage
	{
        //string myId = Application.Current.Properties["myId"].ToString();
        string myName = Application.Current.Properties["myName"].ToString();
        int myScore = Convert.ToInt32(Application.Current.Properties["myScore"]);
        int myLevel = Convert.ToInt32(Application.Current.Properties["myLevel"]);
        private Controllers.PemainController _pemainController = new Controllers.PemainController();

        public ResultPage (int myScore)
		{
			InitializeComponent ();

            labelScore.Text = myScore.ToString();
		}

        void GoToHome(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new HomePage(), false);
        }

        void GoPlayAgain(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PlayPage(), false);
        }

        async void SaveToCloud(object sender, EventArgs e)
        {
            var pemain = new Models.Pemain();

            Debug.WriteLine("nama: {0} | mudah: {1} | sedang: {2} | sulit: {3}", pemain.Nama, pemain.SkorMudah, pemain.SkorSedang, pemain.SkorSulit);

            if (myLevel == 0)
            {
                pemain = new Models.Pemain()
                {
                    //Id = myId,
                    Nama = myName,
                    SkorMudah = myScore,
                };
            }
            else if (myLevel == 1)
            {
                pemain = new Models.Pemain()
                {
                    //Id = myId,
                    Nama = myName,
                    SkorSedang = myScore,
                };
            }
            else if (myLevel == 2)
            {
                pemain = new Models.Pemain()
                {
                    //Id = myId,
                    Nama = myName,
                    SkorSulit = myScore,
                };
            }

            try
            {
                await _pemainController.SavePemainAsync(pemain);
                await DisplayAlert("Keterangan", "Data berhasil disimpan!", "Tutup");
            }
            catch (Exception err)
            {
                await DisplayAlert("Keterangan", "Data gagal disimpan. \nCek koneksi Internet", "Tutup");
                Debug.WriteLine("Error wa. " + err.Message);
            }
        }
    }
}