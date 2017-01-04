using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Maps.Android;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(DevDaysSpeakers.View.CustomMap), typeof(DevDaysSpeakers.Droid.CustomMapRenderer))]
namespace DevDaysSpeakers.Droid
{
	public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter, IOnMapReadyCallback
	{
		private GoogleMap _map = null;
		private DevDaysSpeakers.View.CustomMap _customMap = null;


		protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				/// remove the event handler for the info window
				_map.InfoWindowClick -= OnMapInfoWindowClick;
			}

			if (e.NewElement != null)
			{
				/// get a reference to the CustomMap that was
				/// in the xaml declaration
				_customMap = (View.CustomMap) e.NewElement;
				((MapView) Control).GetMapAsync(this);
			}
		}


		/// <summary>
		/// execute this code if the user taps the info window.
		/// Note that we make use of the INavigationService that was passed
		/// through a binding in the view model.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnMapInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
		{
			if (_customMap.NavigationService != null)
			{
				_customMap.NavigationService.NavigateAsync(DevDaysSpeakers.PageKeys.Speakers);
			}
		}


		/// <summary>
		/// take note of all of the changes to the databinding properties and
		/// take note of any involving the CustomPins property that we created
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (Element == null || Control == null)
				return;

			if (e.PropertyName == View.CustomMap.CustomPinsProperty.PropertyName)
			{
				UpdatePins();
			}
		}



		/// <summary>
		/// if the user taps on the map marker, we will try and pull up the
		/// layout file that was defined in the resources.
		/// </summary>
		/// <param name="marker"></param>
		/// <returns></returns>
		public Android.Views.View GetInfoContents(Marker marker)
		{
			var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
			if (inflater != null)
			{
				Android.Views.View view = null;
				var pin = GetCustomPin(marker);
				if (pin != null)
				{
					view = inflater.Inflate(Resource.Layout.CustomInfoWindow, null);
					var infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle);
					var infoSubTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowSubtitle);

					if (infoTitle != null)
						infoTitle.Text = pin.Pin.Label;

					if (infoSubTitle != null)
						infoSubTitle.Text = pin.Pin.Address;

					return view;
				}
			}

			return null;
		}


		/// <summary>
		/// gets the info window, we will just let it do its own thing
		/// </summary>
		/// <param name="marker"></param>
		/// <returns></returns>
		public Android.Views.View GetInfoWindow(Marker marker)
		{
			return null;
		}


		/// <summary>
		/// called when the map is ready for use, we can save a reference to
		/// the underlying google map and wire up the event handler.
		/// </summary>
		/// <param name="googleMap"></param>
		public void OnMapReady(GoogleMap googleMap)
		{
			_map = googleMap;
			_map.InfoWindowClick += OnMapInfoWindowClick;
			_map.SetInfoWindowAdapter(this);

			UpdatePins();
		}


		/// <summary>
		/// updates the pins on the map
		/// </summary>
		private void UpdatePins()
		{
			if (_map == null)
				return;


			_map.Clear();
			foreach(var pin in _customMap.CustomPins)
			{
				var marker = new MarkerOptions();
				marker.SetPosition(new LatLng(pin.Pin.Position.Latitude, pin.Pin.Position.Longitude));
				marker.SetTitle(pin.Pin.Label);
				marker.SetSnippet(pin.Pin.Address);
				marker.SetIcon(BitmapDescriptorFactory.DefaultMarker(210));
				_map.AddMarker(marker);
			}
		}

		/// <summary>
		/// retrieves the CustomPin object based on the supplied marker.
		/// </summary>
		/// <param name="marker"></param>
		/// <returns></returns>
		private View.CustomPin GetCustomPin(Marker marker)
		{
			var ret = (from p in _customMap.CustomPins
					   where p.Pin.Label == marker.Title
					   select p)
					   .FirstOrDefault();
			return ret;
		}
	}
}