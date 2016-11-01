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
#if DEBUG
			/// testing view models etc, use dummy data source
			Container.RegisterType<Services.ISpeakersService, Services.SpeakersService>();
#else
			/// use production data source
			Container.RegisterType<Services.ISpeakersService, Services.WebApiSpeakersService>();
#endif
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
