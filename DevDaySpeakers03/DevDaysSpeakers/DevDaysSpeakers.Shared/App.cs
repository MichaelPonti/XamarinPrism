using DevDaysSpeakers.View;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Microsoft.Practices.Unity;


namespace DevDaysSpeakers
{
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
			Container.RegisterType<Services.ISpeakersService, Services.SpeakersService>();
			Container.RegisterTypeForNavigation<View.SpeakersPage, ViewModel.SpeakersViewModel>();
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
