using DevDaysSpeakers.View;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Microsoft.Practices.Unity;
using Prism.Navigation;

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
			NavigationParameters nps = new NavigationParameters();
			nps.Add("first", true);
			NavigationService.NavigateAsync(PageKeys.Speakers, nps);
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
			Container.RegisterTypeForNavigation<View.SpeakersPage, ViewModel.SpeakersViewModel>(PageKeys.Speakers);
			Container.RegisterTypeForNavigation<View.DetailsPage, ViewModel.DetailsViewModel>(PageKeys.Details);
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
