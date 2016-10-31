using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevDaysSpeakers.Model;
using Newtonsoft.Json;
using System.Net.Http;

namespace DevDaysSpeakers.Services
{
	public class SpeakersService : ISpeakersService
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
	}
}
