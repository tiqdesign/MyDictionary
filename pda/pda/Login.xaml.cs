using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using pda.Model;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace pda
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
	    private bool act = false;
	    private string emailReg =
	        "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&\'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$";
        public Login ()
		{
			InitializeComponent ();
		}

	    private async void Btn_login_OnClicked(object sender, EventArgs e)
	    {
	        var db = DependencyService.Get<ISQLite>().GetConnection();
	        db.CreateTable<User>();
        

	        if (db.Table<User>().Any(c=>c.mail==tb_mail.Text)) 
	        {
	            if (db.Table<User>().First(c => c.mail == tb_mail.Text).password != tb_password.Text)
	            {
	                await DisplayAlert("Error", "Şifreniz hatalıdır!", "Geri");
	                tb_password.Text = "";
	            }
	            else
	            {
	                var action = await DisplayAlert("Login", "Hoşgeldiniz " + tb_mail.Text, "Giriş Yap", "Geri");
	                if (action)
	                {
	                    await Navigation.PushAsync(new MainPage());
	                    tb_mail.Text = "";
	                    tb_password.Text = "";
	                }
	                else
	                {
	                    await Navigation.PopAsync();
	                }
                }
	            
            }
	        else
	        {
	            User user = new User()
	            {
	                mail = tb_mail.Text,
	                password = tb_password.Text
	            };
	            db.Insert(user);

	            var action = await DisplayAlert("Login", "Kayıt Yapıldı", "Giriş Yap", "Geri");

	            if (action)
	            {
	                await Navigation.PushAsync(new MainPage());
	                tb_mail.Text = "";
	                tb_password.Text = "";
                }
	            else
	            {
	                await Navigation.PopAsync();
	            }
            }

	        
	    }

	    private void Tb_mail_OnCompleted(object sender, EventArgs e)
	    {
	        if (!Regex.IsMatch(tb_mail.Text,emailReg))
	        {
	            lb_mail.IsVisible = true;
	            btn_login.IsEnabled = false;
	            act = false;
	        }
	        else
	        {
	            lb_mail.IsVisible = false;
	            act = true;
            }
	    }

	    private void Tb_password_OnCompleted(object sender, EventArgs e)
	    {
	        char[] a = tb_password.Text.ToCharArray();

	        if (a.Length<5)
	        {
	            lb_pass.IsVisible = true;
	            btn_login.IsEnabled = false;
            }
	        else if (act == true && a.Length>=5)
	        {
	            lb_pass.IsVisible = false;
	            btn_login.IsEnabled = true;
	        }

        }
	}
}