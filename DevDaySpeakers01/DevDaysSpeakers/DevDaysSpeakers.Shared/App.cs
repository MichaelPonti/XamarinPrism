using DevDaysSpeakers.View;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace DevDaysSpeakers
{
    //public class App : Application
    //{
    //    public App()
    //    {
    //        // The root page of your application
    //        var content = new SpeakersPage();

    //        MainPage = new NavigationPage(content);
    //    }

    //    protected override void OnStart()
    //    {
    //        // Handle when your app starts
    //    }

    //    protected override void OnSleep()
    //    {
    //        // Handle when your app sleeps
    //    }

    //    protected override void OnResume()
    //    {
    //        // Handle when your app resumes
    //    }
    //}

	public class App : PrismApplication
	{
		public App(IPlatformInitializer initializer = null)
			: base(initializer)
		{
		}

		protected override void OnInitialized()
		{
			NavigationService.NavigateAsync("SpeakersPage");
		}

		protected override void RegisterTypes()
		{
			Container.RegisterTypeForNavigation<View.SpeakersPage>();
		}

		protected override void OnSleep()
		{
			base.OnSleep();
		}

		protected override void OnResume()
		{
			base.OnResume();
		}
	}
}
