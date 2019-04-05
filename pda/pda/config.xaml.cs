using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pda.Model;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace pda
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class config : ContentPage
    {
     
        public config()
        {
            InitializeComponent();
            var db = DependencyService.Get<ISQLite>().GetConnection();
            db.CreateTable<Word>();
            pk_word.ItemsSource = db.Table<Word>().Select(l => l.word_en).ToList();
        }

        private async void Btn_add_OnClicked(object sender, EventArgs e)
        {
            var db = DependencyService.Get<ISQLite>().GetConnection();


            if (String.IsNullOrWhiteSpace(tb_tr.Text) || String.IsNullOrWhiteSpace(tb_en.Text))
            {
                await DisplayAlert("Error", "Please enter two types of word", "OK");
            }
            else if (db.Table<Word>().Any(c => c.word_en == tb_en.Text) || db.Table<Word>().Any(v => v.word_tr == tb_tr.Text))
            {
                await DisplayAlert("Error", "These words already exist in this dictionary!", "OK");
            }
            else
            {
                Word word1 = new Word()
                {
                    word_tr = tb_tr.Text,
                    word_en = tb_en.Text
                };
                db.Insert(word1);
                pk_word.ItemsSource = db.Table<Word>().Select(l => l.word_en).ToList();
                await DisplayAlert("OK", "These words added to this dictionary", "OK");
            }
        }

        private async void Btn_delete_OnClicked(object sender, EventArgs e)
        {
            var db = DependencyService.Get<ISQLite>().GetConnection();

            var countEn = db.Table<Word>().Any(co => co.word_en == tb_en.Text);
            var countTr = db.Table<Word>().Any(co => co.word_tr == tb_tr.Text);

            if ((String.IsNullOrWhiteSpace(tb_tr.Text) && String.IsNullOrWhiteSpace(tb_en.Text)) || (!String.IsNullOrWhiteSpace(tb_tr.Text)) && !String.IsNullOrWhiteSpace(tb_en.Text))
            {
                await DisplayAlert("Error", "Please enter just one word for delete", "OK");
            }

            else if (String.IsNullOrWhiteSpace(tb_tr.Text) && countEn)
            {
                var deletedWord = db.Table<Word>().First(d => d.word_en == tb_en.Text);
                db.Delete(deletedWord);
                await DisplayAlert("OK", "These words deleted from this dictionary", "OK");
                
            }

            else if (String.IsNullOrWhiteSpace(tb_en.Text) && countTr)
            {
                var deletedWord = db.Table<Word>().First(d => d.word_tr == tb_tr.Text);
                db.Delete(deletedWord);
                await DisplayAlert("OK", "These words deleted from this dictionary", "OK");
               
            }
            else
            {
                await DisplayAlert("Error", "This dictionary does not contain this word", "OK");
            }

            pk_word.ItemsSource = db.Table<Word>().Select(l => l.word_en).ToList();
        }

        private async void Btn_update_OnClicked(object sender, EventArgs e)
        {
            var db = DependencyService.Get<ISQLite>().GetConnection();


            if (String.IsNullOrWhiteSpace(tb_tr.Text) || String.IsNullOrWhiteSpace(tb_en.Text))
            {
                await DisplayAlert("Error", "Please enter two types of word", "OK");
            }
            else if ((!String.IsNullOrWhiteSpace(tb_tr.Text) && !String.IsNullOrWhiteSpace(tb_en.Text))&& db.Table<Word>().Any(co => co.word_en == tb_en.Text)!=true)
            {
                await DisplayAlert("Error", "This dictionary does not contain this word", "OK");
            }
            else
            {
                var updated = db.Table<Word>().First(u => u.word_en == tb_en.Text);
                updated.word_tr = tb_tr.Text;
                db.Update(updated);
                await DisplayAlert("Done", "The word is Updated", "OK");
             
            }
        }

        private void Picker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var db = DependencyService.Get<ISQLite>().GetConnection();
            if (pk_word.SelectedIndex != -1)
            {
                var wordEng = pk_word.Items[pk_word.SelectedIndex];
                var wordObj = db.Table<Word>().First(o => o.word_en == wordEng);
                tb_tr.Text = wordObj.word_tr;
                tb_en.Text = wordObj.word_en;
            }
            else
            {
                tb_tr.Text = null;
                tb_en.Text = null;
                pk_word.SelectedIndex = -1;
            }
        }

       
    }
}