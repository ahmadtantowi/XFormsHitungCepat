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
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();
		}

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                btnPlay.IsEnabled = true;
                Debug.WriteLine("Level yang dipilih = {0}", selectedIndex);
            }

            Application.Current.Properties["myLevel"] = selectedIndex;
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            var myBtn = (Button)sender;

            switch (myBtn.Text)
            {
                case "Main":
                    Navigation.PushModalAsync(new PlayPage(), true);
                    break;
                case "Papan Skor":
                    Navigation.PushModalAsync(new RankingPage(), true);
                    break;
            }

            Application.Current.Properties["myName"] = myName.Text;
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}