using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MapKit;

namespace DevDaysSpeakers.iOS
{
	public class CustomMKAnnotationView : MKAnnotationView
	{
		public string Id { get; set; }
		public string Url { get; set; }

		public CustomMKAnnotationView()
		{
		}

		public CustomMKAnnotationView(IMKAnnotation annotation, string id, string url)
			: base(annotation, id)
		{
			Id = id;
			Url = url;
		}
	}
}