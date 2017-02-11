using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace DevDaysSpeakers.View
{
	public class CustomPin
	{
		public Pin Pin { get; set; }
		public int Id { get; set; }
		public string Url { get; set; }

	}


	public class CustomMap : Map
	{
		public static readonly BindableProperty CustomPinsProperty =
			BindableProperty.Create(
				"CustomPins",
				typeof(List<CustomPin>),
				typeof(CustomMap),
				new List<CustomPin>(),
				BindingMode.TwoWay);

		public List<CustomPin> CustomPins
		{
			get { return (List<CustomPin>) GetValue(CustomPinsProperty); }
			set { SetValue(CustomPinsProperty, value); }
		}



		public static readonly BindableProperty NavigationServiceProperty =
			BindableProperty.Create(
				"NavigationService",
				typeof(Prism.Navigation.INavigationService),
				typeof(CustomMap),
				null);


		public Prism.Navigation.INavigationService NavigationService
		{
			get { return (Prism.Navigation.INavigationService) GetValue(NavigationServiceProperty); }
			set { SetValue(NavigationServiceProperty, value); }
		}
	}
}
