using System;
using System.Collections.Generic;
using System.Text;
using Prism.Navigation;
using Xamarin.Forms;

namespace DevDaysSpeakers.ViewModel
{
	public class MapViewModel : BaseViewModel
	{

		public MapViewModel(INavigationService navigationService)
			: base()
		{
			NavService = navigationService;
		}


		/// <summary>
		/// pin list for our map
		/// </summary>
		private List<View.CustomPin> _customPins = new List<View.CustomPin>();
		public List<View.CustomPin> CustomPins
		{
			get { return _customPins; }
			set { SetProperty<List<View.CustomPin>>(ref _customPins, value); }
		}


		/// <summary>
		/// going to bind the nav service to our custom map so that
		/// we can use it in our custom renderer
		/// </summary>
		private INavigationService _navService = null;
		public INavigationService NavService
		{
			get { return _navService; }
			set { SetProperty<INavigationService>(ref _navService, value); }
		}


		public override void OnNavigatedTo(NavigationParameters parameters)
		{
			base.OnNavigatedTo(parameters);

			/// upon navigating to the page, lets load up a custom pin
			/// that the map can use.
			List<View.CustomPin> newPins = new List<View.CustomPin>();

			var p1 = new View.CustomPin
			{
				Pin = new Xamarin.Forms.Maps.Pin
				{
					Address = "address 1",
					Label = "Label 1",
					Position = new Xamarin.Forms.Maps.Position(49.3165, -123.0724),
				},
				Id = 1,
				Url = "http://google.ca"
			};

			newPins.Add(p1);

			/// custom pins is a bindable property for our custom map
			CustomPins = newPins;
		}


		private Command _commandTest = null;
		public Command CommandTest
		{
			get
			{
				return _commandTest ??
					(_commandTest = new Command(() =>
					{
						List<View.CustomPin> newPins = new List<View.CustomPin>();

						var p1 = new View.CustomPin
						{
							Pin = new Xamarin.Forms.Maps.Pin
							{
								Address = "address 1a",
								Label = "Label 1a",
								Position = new Xamarin.Forms.Maps.Position(49.3162, -123.0720),
							},
							Id = 1,
							Url = "http://google.ca"
						};

						newPins.Add(p1);

						/// custom pins is a bindable property for our custom map
						CustomPins = newPins;
					}));
			}
		}
	}
}
