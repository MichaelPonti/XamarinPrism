﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevDaysSpeakers.Model;
using Newtonsoft.Json;
using System.Net.Http;

namespace DevDaysSpeakers.Services
{
	/// <summary>
	/// use this class for your ISpeakersService implementation while testing
	/// </summary>
	public class SpeakersService : ISpeakersService
	{
		private const string JsonResponse =
			"[\r\n\r\n      {\r\n        \"Name\": \"Matthew Soucoup\",\r\n        \"Description\": \"Matthew is a Xamarin MVP and Certified Xamarin Developer from Madison, WI. He founded his company Code Mill Technologies and started the Madison Mobile .Net Developers Group.  Matt regularly speaks on .Net and Xamarin development at user groups, code camps and conferences throughout the Midwest. Matt gardens hot peppers, rides bikes, and loves Wisconsin micro-brews and cheese.\",\r\n        \"Image\": \"http://i.imgur.com/y4dzyT3.jpg\",\r\n        \"Title\": \"Architect\",\r\n        \"Company\": \"Code Mill Technologies\",\r\n        \"Website\": \"https://codemilltech.com\",\r\n        \"Blog\": \"https://codemilltech.com/\",\r\n        \"Twitter\": \"codemillmatt\",\r\n        \"Email\": \"MSoucoup@newco.com\",\r\n        \"Avatar\": \"http://i.imgur.com/RTDt4nb.jpg\"\r\n      },\r\n      {\r\n        \"Name\": \"Star Simpson\",\r\n        \"Description\": \"A robot-builder from an early age, Star has explored robotics and automation in electronics and software from MIT to Shenzhen. She previously worked on some of the first robots to demonstrate human emotional expressiveness in Cynthia Brezeal’s personal robotics lab. Her interest carried her into the aerial robotics world, exploring drone-based delivery through TacoCopter many years ahead of anyone else. Now residing in SF, she’s looking for ways that tech can advance and extend human capability.\",\r\n        \"Image\": \"http://i.imgur.com/mqRwv84.jpg\",\r\n        \"Title\": \"Consultant\",\r\n        \"Company\": \"\",\r\n        \"Website\": \"N/A\",\r\n        \"Blog\": \"N/A\",\r\n        \"Twitter\": \"starsandrobots\",\r\n        \"Email\": \"SSimpson@newco.com\",\r\n        \"Avatar\": \"http://i.imgur.com/BlC5zlJ.jpg\"\r\n      },\r\n      {\r\n        \"Name\": \"Sam Tulimat \",\r\n        \"Description\": \"Sam works on the Microsoft Intune team at Microsoft’s Boston office. He developed a love for technology when he started an internet Café in his home country of Syria. Later on he decided to bring his love for Entrepreneurship and tech to Microsoft by focusing on the fast evolving technology for app management and data protection in the enterprise.\",\r\n        \"Image\": \"http://i.imgur.com/nDyflPa.jpg\",\r\n        \"Title\": \"Enterprise Mobility Program Manager\",\r\n        \"Company\": \"Microsoft\",\r\n        \"Website\": \"http://microsoft.com\",\r\n        \"Blog\": \"N/A\",\r\n        \"Twitter\": \"Samtulim\",\r\n        \"Email\": \"STulimat@newco.com\",\r\n        \"Avatar\": \"http://i.imgur.com/ywPLGwp.jpg\"\r\n      },\r\n      {\r\n        \"Name\": \"Peter Friese\",\r\n        \"Description\": \"Peter is a developer advocate at Google where he is responsible for Google's relationship with Xamarin. He's the author of the first ever Android Watchface written in C# and an avid collector of smart watches. He likes a good cup of English breakfast tea (milk, no sugar), any time of day.\",\r\n        \"Image\": \"http://i.imgur.com/MTn1vJ8.jpg\",\r\n        \"Title\": \"Developer Advocate\",\r\n        \"Company\": \"Google\",\r\n        \"Website\": \"http://google.com\",\r\n        \"Blog\": \"http://www.peterfriese.de\",\r\n        \"Twitter\": \"peterfriese\",\r\n        \"Email\": \"PFriese@newco.com\",\r\n        \"Avatar\": \"http://i.imgur.com/FKOslOv.jpg\"\r\n      },\r\n      {\r\n        \"Name\": \"Jonathan Dick\",\r\n        \"Description\": \"Jonathan Dick is developer who hails from The Great White North and has been slinging C# for mobile since it was born. A self-proclaimed expert on Push Notifications, Jonathan has published several Xamarin Components, and even helped write a book on Android C# programming. \",\r\n        \"Image\": \"http://i.imgur.com/CvKs7dV.jpg\",\r\n        \"Company\": \"Xamarin\",\r\n        \"Website\": \"http://xamarin.com\",\r\n        \"Blog\": \"http://redth.codes\",\r\n        \"Twitter\": \"redth\",\r\n        \"Email\": \"JDick@newco.com\",\r\n        \"Avatar\": \"http://i.imgur.com/IrOo2aT.jpg\"\r\n      },\r\n      {\r\n        \"Name\": \"Martijn van Dijk\",\r\n        \"Description\": \"Martijn is a Xamarin consultant at Xablu, Xamarin MVP, contributor of MvvmCross, and creator of several Xamarin plugins.\",\r\n        \"Image\": \"http://i.imgur.com/xActFqO.jpg\",\r\n        \"Title\": \"Xamarin Consultant\",\r\n        \"Company\": \"Xablu\",\r\n        \"Website\": \"http://www.xablu.com\",\r\n        \"Blog\": \"https://github.com/martijn00\",\r\n        \"Twitter\": \"mhvdijk\",\r\n        \"Email\": \"Mvandijk@newco.com\",\r\n        \"Avatar\": \"http://i.imgur.com/TInfNTf.jpg\"\r\n      },\r\n      {\r\n        \"Name\": \"Michael Stonis\",\r\n        \"Description\": \"Michael Stonis is a partner at Eight-Bot, a software consultancy in Chicago, where he focuses on mobile and integration solutions for enterprises using .NET. He loves mobile technology and how it has opened up our world in new and interesting ways that seemed like an impossibility just a few years ago. Outside of work, you will probably find him spending time with his family, brewing beer, or playing pinball.\",\r\n        \"Image\": \"http://i.imgur.com/y0kI3xo.jpg\",\r\n        \"Title\": \"President\",\r\n        \"Company\": \"EightBot\",\r\n        \"Website\": \"http://www.eightbot.com\",\r\n        \"Blog\": \"http://www.eightbot.com/author/michael-stonis\",\r\n        \"Twitter\": \"MichaelStonis\",\r\n        \"Email\": \"MStonis@newco.com\",\r\n        \"Avatar\": \"http://i.imgur.com/vJQw3kW.jpg\"\r\n      },\r\n      {\r\n        \"Name\": \"Kasey Uhlenhuth\",\r\n        \"Description\": \"Kasey Uhlenhuth is a program manager on the .NET Managed Languages team at Microsoft. She is currently working on modernizing the C# developer experience, but has also worked on C# scripting and the REPL. Before joining Microsoft, Kasey studied computer science and played varsity lacrosse at Harvard University. In her free time she can be found creating art, reading, or playing volleyball and ultimate frisbee.\",\r\n        \"Image\": \"http://i.imgur.com/E38jreN.jpg\",\r\n        \"Title\": \"Program Manager, .NET Managed Languages\",\r\n        \"Company\": \"Microsoft\",\r\n        \"Website\": \"http://microsoft.com\",\r\n        \"Blog\": \"N/A\",\r\n        \"Twitter\": \"Uhlenhuth\",\r\n        \"Email\": \"KUhlenhuth@newco.com\",\r\n        \"Avatar\": \"http://i.imgur.com/N35EeSW.jpg\"\r\n      },\r\n      {\r\n        \"Name\": \"Jason Smith\",\r\n        \"Description\": \"Jason Smith has been enthusiastically working on UI and front-end development for the past decade, touching almost every major UI toolkit in the process (don't worry, his hands are clean). At Xamarin he works on research and development projects. Jason's most recent endeavor at the company has him leading the Xamarin.Forms project to create a true cross-platform UI solution.\",\r\n        \"Image\": \"http://i.imgur.com/VKjZqlZ.jpg\",\r\n        \"Title\": \"Engineering Team Lead (Xamarin.Forms)\",\r\n        \"Company\": \"Xamarin\",\r\n        \"Website\": \"http://xamarin.com\",\r\n        \"Blog\": \"http://xfcomplete.net\",\r\n        \"Twitter\": \"jassmith87\",\r\n        \"Email\": \"JSmith@newco.com\",\r\n        \"Avatar\": \"http://i.imgur.com/JE5DJGV.jpg\"\r\n      },\r\n      {\r\n        \"Name\": \"Cliff Sentell\",\r\n        \"Description\": \"Cliff Sentell is co-founder and Chief Technology Officer for Compass Professional Health Services. A technology innovator, Cliff pioneered the development of the company’s groundbreaking healthcare decision support and pricing transparency platforms. Currently, his focus is on rich, context-aware mobile experiences that humanize healthcare for the 2+ million people who use Compass solutions.\",\r\n        \"Image\": \"http://i.imgur.com/1Ytl9qO.jpg\",\r\n        \"Title\": \"CTO\",\r\n        \"Company\": \"Compass Professional Health Services \",\r\n        \"Website\": \"N/A\",\r\n        \"Blog\": \"N/A\",\r\n        \"Twitter\": \"N/A\",\r\n        \"Email\": \"CSentell@newco.com\",\r\n        \"Avatar\": \"http://i.imgur.com/e5LWsn4.jpg\"\r\n      },\r\n      {\r\n        \"Name\": \"Lori Lalonde\",\r\n        \"biography\": \"Lori Lalonde is an author, consultant, blogger, conference speaker, Microsoft MVP, Xamarin MVP & Certified Mobile Developer. She is a proud member of the Western Devs group, and serves as the User Group Leader of CTTDNUG, a .NET User Group in Kitchener, Ontario. You can follow her on Twitter @loriblalonde, check out her blog at solola.ca, and see what she's been up to with the other Western Devs at westerndevs.com.\",\r\n        \"Image\": \"http://i.imgur.com/QYmlfmB.jpg\",\r\n        \"Title\": \"Senior Consultant\",\r\n        \"Company\": \"ObjectSharp Consulting\",\r\n        \"Website\": \"N/A\",\r\n        \"Blog\": \"http://solola.ca\",\r\n        \"Twitter\": \"loriblalonde\",\r\n        \"Email\": \"LLalonde@newco.com\",\r\n        \"Avatar\": \"http://i.imgur.com/mpfSogm.jpg\"\r\n      },\r\n      {\r\n        \"Name\": \"Paul Betts\",\r\n        \"Description\": \"Paul is a developer at Slack who works on the Windows and Linux application. Previously he was at GitHub where he built the GitHub Desktop application on Windows, as well as the popular Xamarin libraries ReactiveUI, ModernHttpClient, and Akavache.\",\r\n        \"Image\": \"http://i.imgur.com/fQytI8I.jpg\",\r\n        \"Title\": \"Engineer\",\r\n        \"Company\": \"Slack\",\r\n        \"Website\": \"http://slack.com\",\r\n        \"Blog\": \"http://log.paulbetts.org\",\r\n        \"Twitter\": \"paulcbetts\",\r\n        \"Email\": \"PBetts@newco.com\",\r\n        \"Avatar\": \"http://i.imgur.com/jrEEQRe.jpg\"\r\n      },\r\n      {\r\n        \"Name\": \"Josh Clark\",\r\n        \"Description\": \"Josh Clark is the founder of Big Medium, a design agency specializing in connected devices, mobile experiences, and responsive web design. His clients include Samsung, Time Inc, TechCrunch, Entertainment Weekly, eBay, O’Reilly Media, and many others. Josh has written several books, including “Designing for Touch” (A Book Apart, 2015) and “Tapworthy: Designing Great iPhone Apps” (O’Reilly, 2010). He speaks around the world about what’s next for digital interfaces.\\n\",\r\n        \"Image\": \"http://i.imgur.com/Ji9BWje.jpg\",\r\n        \"Title\": \"Founder\",\r\n        \"Company\": \"Big Medium\",\r\n        \"Website\": \"https://bigmedium.com\",\r\n        \"Blog\": \"https://bigmedium.com\",\r\n        \"Twitter\": \"bigmediumjosh\",\r\n        \"Email\": \"JClark@newco.com\",\r\n        \"Avatar\": \"http://i.imgur.com/zs7zuZl.jpg\"\r\n      },\r\n      {\r\n        \"Name\": \"Danny Strockis\",\r\n        \"Description\": \"Danny Strockis is a PM in the Microsoft identity services division.  His team is responsible for all developer related features of the Microsoft identity platform, including protocol design and SDK development across platforms.  Danny is a recent Xamarin enthusiast (both inside & outside of work) and is excited to share the work that Microsoft has done to support Xamarin developers on the Microsoft Cloud.\",\r\n        \"Image\": \"http://i.imgur.com/bzVfPpo.jpg\",\r\n        \"Title\": \"Program Manager, Microsoft Identity Developer Platform\",\r\n        \"Company\": \"Microsoft\",\r\n        \"Website\": \"http://microsoft.com\",\r\n        \"Blog\": \"N/A\",\r\n        \"Twitter\": \"dstrockis\",\r\n        \"Email\": \"DStrockis@newco.com\",\r\n        \"Avatar\": \"http://i.imgur.com/M8qnRPH.jpg\"\r\n        }\r\n\r\n    ]";

		public Task<List<Speaker>> GetAllSpeakersAsync()
		{
			var items = JsonConvert.DeserializeObject<List<Speaker>>(JsonResponse);
			return Task.FromResult<List<Speaker>>(items);
		}


		public Task<Speaker> GetSpeakerAsync(string id)
		{
			var items = JsonConvert.DeserializeObject<List<Speaker>>(JsonResponse);
			var ret = (from n in items where n.Name == id select n).FirstOrDefault();
			return Task.FromResult<Speaker>(ret);
		}
	}


	/// <summary>
	/// Use this class for your ISpeakersService implementation when deploying the app
	/// to productions, store, etc ... not this is necessarily ready for production,
	/// but you can keep different implementations handy.
	/// </summary>
	public class WebApiSpeakersService : ISpeakersService
	{
		public async Task<List<Speaker>> GetAllSpeakersAsync()
		{
			using (var client = new HttpClient())
			{
				//grab json from server
				var json = await client.GetStringAsync("http://demo4404797.mockable.io/speakers");

				//Deserialize json
				var items = JsonConvert.DeserializeObject<List<Speaker>>(json);
				return items;
			}
		}

		public async Task<Speaker> GetSpeakerAsync(string id)
		{
			var items = await GetAllSpeakersAsync();
			return (from n in items where n.Name == id select n).FirstOrDefault();
		}
	}
}