using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using DevDaysSpeakers.View;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using MapKit;
using System.ComponentModel;

[assembly:ExportRenderer(typeof(DevDaysSpeakers.View.CustomMap), typeof(DevDaysSpeakers.iOS.CustomMapRenderer))]
namespace DevDaysSpeakers.iOS
{
	public class CustomMapRenderer : MapRenderer
	{
		private MKMapView _nativeMap = null;
		private UIView _customPinView = null;
		private List<CustomPin> _customPins = null;
		private CustomMap _customMap = null;

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				var nativeMap = Control as MKMapView;
				nativeMap.GetViewForAnnotation = null;
				nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
				nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
				nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;
				_nativeMap = null;
			}

			if (e.NewElement != null)
			{
				_customMap = (CustomMap) e.NewElement;
				_nativeMap = Control as MKMapView;
				_customPins = _customMap.CustomPins;

				_nativeMap.GetViewForAnnotation = GetViewForAnnotation;
				_nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
				_nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
				_nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
			}

		}


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

		private void UpdatePins()
		{
			if (_nativeMap == null)
				return;

			foreach(var p in _customMap.CustomPins)
			{
				MKPointAnnotation pa = new MKPointAnnotation
				{
					Coordinate = new CoreLocation.CLLocationCoordinate2D(p.Pin.Position.Latitude, p.Pin.Position.Longitude),
					Subtitle = p.Pin.Address,
					Title = p.Pin.Label,
				};
				_nativeMap.AddAnnotation(pa);
			}
		}

		private MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
		{
			if (annotation is MKUserLocation)
				return null;

			MKAnnotationView ret = null;

			var pointAnnotation = annotation as MKPointAnnotation;
			var customPin = GetCustomPin(pointAnnotation);
			if (customPin == null)
				throw new Exception("My pin not found");

			ret = mapView.DequeueReusableAnnotation(customPin.Id.ToString());
			if (ret == null)
			{
				ret = new CustomMKAnnotationView(annotation, customPin.Id.ToString(), customPin.Url);
				ret.Image = UIImage.FromFile("custompin2.png");
				ret.CalloutOffset = new CoreGraphics.CGPoint(0, 0);
				ret.LeftCalloutAccessoryView = new UIImageView(UIImage.FromFile("custompinimage.png"));
				ret.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
				((CustomMKAnnotationView) ret).Id = customPin.Id.ToString();
				((CustomMKAnnotationView) ret).Url = customPin.Url;
			}

			ret.CanShowCallout = true;

			return ret;
		}

		private CustomPin GetCustomPin(MKPointAnnotation pa)
		{
			foreach(var p in _customMap.CustomPins)
			{
				if (pa.Title == p.Pin.Label)
				{
					return p;
				}
			}

			return null;
		}

		private void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
		{
			if (!e.View.Selected)
			{
				_customPinView.RemoveFromSuperview();
				_customPinView.Dispose();
				_customPinView = null;
			}
		}

		private void OnCalloutAccessoryControlTapped(object sender, MKMapViewAccessoryTappedEventArgs e)
		{
			var customView = e.View as CustomMKAnnotationView;
			if (!String.IsNullOrWhiteSpace(customView.Url))
			{
				UIApplication.SharedApplication.OpenUrl(new Foundation.NSUrl(customView.Url));
			}
		}

		private void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
		{
			var customView = e.View as CustomMKAnnotationView;
			_customPinView = new UIView();

			if (customView.Id == "1")
			{
				_customPinView.Frame = new CoreGraphics.CGRect(0, 0, 200, 84);
				var image = new UIImageView(new CoreGraphics.CGRect(0, 0, 200, 84));
				image.Image = UIImage.FromFile("custommapimage.png");
				_customPinView.AddSubview(image);
				_customPinView.Center = new CoreGraphics.CGPoint(0, -(e.View.Frame.Height + 75));
				e.View.AddSubview(_customPinView);
			}
		}
	}
}