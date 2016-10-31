using DevDaysSpeakers.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace DevDaysSpeakers.Services
{
	public interface ISpeakersService
	{
		Task<List<Speaker>> GetAllSpeakersAsync();
	}
}
