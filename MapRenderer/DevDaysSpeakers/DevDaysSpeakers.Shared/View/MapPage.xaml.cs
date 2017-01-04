using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace DevDaysSpeakers.View
{
	public partial class MapPage : ContentPage
	{
		public MapPage ()
		{
			InitializeComponent ();

			/// I am just going to center the map to a park
			/// near home. I could have done this by using 
			/// an attached property and expose the map location
			/// to the view model, but you can look that up if
			/// you want: https://randombitsandbytes.com/2016/10/02/xamarin-forms-map-center/
			cusmap.MoveToRegion(new MapSpan(new Position(49.3174, -123.0744), 0.01, 0.01));
		}
	}
}
