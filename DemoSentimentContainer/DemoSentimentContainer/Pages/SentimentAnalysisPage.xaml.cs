using System;

using DemoSentimentContainer.Helpers;
using DemoSentimentContainer.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoSentimentContainer.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SentimentAnalysisPage : ContentPage
	{
        public SentimentAnalysisPage()
        {
            InitializeComponent();
        }
        private async void AnalyzeButton_Clicked(object sender, EventArgs e)
        {
            Loading(true);

            var sentiment = await TextAnalyticsService.AnalyzeText(MessageEntry.Text, LanguageEntry.Text);

            if (sentiment != null)
            {
                ScoreLabel.Text = sentiment.score.ToString("N4");
                ScoreLabel.TextColor = SentimentColor.Retrieve(sentiment.score);
            }

            Loading(false);
        }

        void Loading(bool mostrar)
        {
            indicator.IsEnabled = mostrar;
            indicator.IsRunning = mostrar;
        }
    }
}