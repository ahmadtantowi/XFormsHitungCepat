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
	public partial class PlayPage : ContentPage
	{
        bool isEnd = false;
        int val1 = 0, val2 = 0, result = 0, myScore = 0, totalQuestion = 0;
        int level = (int)Application.Current.Properties["myLevel"];
        char[] opr = { '+', '-', 'x', '/', '?'};

        Random rnd = new Random();

        public PlayPage ()
		{
			InitializeComponent ();

            GetQuestion();
            StartCountdownHealth(new TimeSpan(0, 0, 0, 0, 1));

            labelAnswer.Completed += (sender, e) => GetResult();
        }

        async void StartCountdownHealth(TimeSpan interval)
        {
            while (prgHealth.Progress >= 0 && isEnd == false)
            {
                prgHealth.Progress = prgHealth.Progress - 0.0005; 
                await Task.Delay(interval);

                if (prgHealth.Progress == 0)
                {
                    isEnd = true;
                    EndPlay();
                }
            }
        }

        void GetQuestion()
        {

            // level 0=mudah, 1=sedang, 2=sulit. ditambah dua untuk ambil operator (opr) aritmatika secara acak
            opr[4] = (char)opr.GetValue(rnd.Next(0, level + 2));

            if (opr[4] == '+')
            {
                val1 = rnd.Next(1, 100);
                val2 = rnd.Next(1, 100);
                result = val1 + val2;
            }
            else if (opr[4] == '-')
            {
                val1 = rnd.Next(1, 100);
                val2 = rnd.Next(1, val1 + 1);
                result = val1 - val2;
            }
            else if (opr[4] == 'x')
            {
                val1 = rnd.Next(1, 100);
                val2 = rnd.Next(1, 10);
                result = val1 * val2;
            }
            else if (opr[4] == '/')
            {
                val1 = rnd.Next(1, 100);
                val2 = rnd.Next(1, 10);
                result = val1 / val2;
            }

            totalQuestion++;
            Debug.WriteLine("Pertanyaan ke-{0} ", totalQuestion);

            labelQuest.Text = val1.ToString() + " " + opr[4] + " " + val2.ToString();
            labelAnswer.Text = "";
            labelAnswer.Focus();
        }

        void GetResult()
        {
            double currentHealth = prgHealth.Progress;

            if (labelAnswer.Text == result.ToString())
            {
                prgHealth.Progress = prgHealth.Progress + 0.1;
                myScore++;
            }
            Debug.WriteLine("Skor sementara {0}", myScore);

            GetQuestion();
        }

        void EndPlay()
        {
            isEnd = true;

            Application.Current.Properties["myScore"] = myScore;
            Navigation.PushModalAsync(new ResultPage(myScore), true);
        }

        private async void OnButtonPlayPageClicked(object sender, EventArgs e)
        {
            var myBtn = (Button)sender;

            switch (myBtn.Text)
            {
                case "Cek":
                    GetResult();
                    break;
                case "Nyerah":
                    var action = await DisplayAlert("Nyerah?", "Akhiri permainan", "Ya", "Batal");
                    if (action) EndPlay();
                    break;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}