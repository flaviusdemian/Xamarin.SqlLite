using System;
using System.IO;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Hangman.Helpers;
using HangmanCore.BussinessLogic;

namespace Hangman
{
    [Activity(Label = "Hangman", MainLauncher = true, Icon = "@drawable/icon")]
    public class SplashScreenActivity : Activity
    {
        int count = 1;
        private WordHelper wordHerlper;
        private DbManager dbManager;
        public static Activity CurrentActivity { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            //SetContentView(Resource.Layout.Login);

            try
            {
                // Get our button from the layout resource,
                // and attach an event to it
                Button button = FindViewById<Button>(Resource.Id.MyButton);

                button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

                CurrentActivity = this;

                dbManager = new DbManager();
                wordHerlper = new WordHelper(dbManager);
                dbManager.InitDatabase();

                //plusClient = new PlusClient.Builder(this, this, this).Build(); 
                //googleLoginButton = FindViewById<SignInButton>(Resource.Id.sign_in_button); 
                //plusOneButton = FindViewById<PlusOneButton> (Resource.Id.plus_one_button);

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}

