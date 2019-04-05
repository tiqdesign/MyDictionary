using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pda.Model;
using SQLite;
using Xamarin.Forms;

namespace pda
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
         
        }

        private async void Btn_conf_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new config());
        }

        private void Btn_change_OnClicked(object sender, EventArgs e)
        {
            switch (btn_change.Text)
            {
                case "Tr>En":
                    btn_change.Text = "En>Tr";
                    break;
                case "En>Tr":
                    btn_change.Text = "Tr>En";
                    break;
            }
        }

        private void Btn_search_OnClicked(object sender, EventArgs e)
        {
            var db = DependencyService.Get<ISQLite>().GetConnection();
            switch (btn_change.Text)
            {
                case "Tr>En":
                    if (db.Table<Word>().Any(w => w.word_tr == tb_word.Text))
                    {
                        lb_translate.Text = "-"+db.Table<Word>().First(w => w.word_tr == tb_word.Text).word_en;
                    }
                    else
                    {
                        lb_translate.Text = "-We can not find this word in dictionary";
                    }
                    break;

                case "En>Tr":
                    if (db.Table<Word>().Any(w => w.word_en == tb_word.Text))
                    {
                        lb_translate.Text = "-"+db.Table<Word>().First(w => w.word_en == tb_word.Text).word_tr;
                    }
                    else
                    {
                        lb_translate.Text = "-We can not find this word in dictionary";
                    }
                    break;
            }
        }
    }
}
