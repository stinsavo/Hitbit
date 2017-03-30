using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Hitbit.Db;
using Hitbit.Models;

namespace Hitbit.Views
{

    public sealed partial class ReadyPhrase : Page
    {
        public ReadyPhrase()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            using (HitbitContext db = new HitbitContext())
            {
                PhrasesList.ItemsSource = db.Phrase.ToList();
            }
        }
        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (PhrasesList.SelectedItem != null)
            {
                Phrase removePhrase = PhrasesList.SelectedItem as Phrase;
                if (removePhrase != null)
                {
                    using (HitbitContext db = new HitbitContext())
                    {
                        db.Phrase.Remove(removePhrase);
                        db.SaveChanges();
                        PhrasesList.ItemsSource = db.Phrase.ToList();
                    }
                }
            }
        }
    }
}
