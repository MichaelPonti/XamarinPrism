using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using DevDaysSpeakers.Model;
using DevDaysSpeakers.Services;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace DevDaysSpeakers.ViewModel
{
	public class SpeakersViewModel : BaseViewModel
	{
		private Services.ISpeakersService _speakersService = null;

		public SpeakersViewModel(Services.ISpeakersService speakersService)
			: base()
		{
			_speakersService = speakersService;
		}

		public ObservableCollection<Speaker> Speakers { get; set; } =
			new ObservableCollection<Speaker>();

		private Speaker _selectedItem = null;
		public Speaker SelectedItem
		{
			get { return _selectedItem; }
			set { SetProperty<Speaker>(ref _selectedItem, value); }
		}


		private Command _getSpeakersCommand = null;
		public Command GetSpeakersCommand
		{
			get
			{
				return _getSpeakersCommand ??
					(_getSpeakersCommand = new Command(
						async () => await GetSpeakers(),
						() => !IsBusy));
			}
		}



		private async Task GetSpeakers()
		{
			if (IsBusy)
				return;

			Exception error = null;
			try
			{
				IsBusy = true;

				var items = await _speakersService.GetAllSpeakersAsync();
				Speakers.Clear();
				foreach (var item in items)
					Speakers.Add(item);
			}
			catch (Exception ex)
			{
				error = ex;
			}
			finally
			{
				IsBusy = false;
			}

			if (error != null)
				await Application.Current.MainPage.DisplayAlert("Error!", error.Message, "OK");
		}

	}
}
