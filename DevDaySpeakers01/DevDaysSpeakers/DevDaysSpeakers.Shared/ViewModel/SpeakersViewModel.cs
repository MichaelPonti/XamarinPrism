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
	public class SpeakersViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] string name = null) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


		private bool _isBusy = false;
		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				_isBusy = value;
				OnPropertyChanged();
				GetSpeakersCommand.ChangeCanExecute();
			}
		}


		public ObservableCollection<Speaker> Speakers { get; set; } = 
			new ObservableCollection<Speaker>();


		private async Task GetSpeakers()
		{
			if (IsBusy)
				return;

			Exception error = null;
			try
			{
				IsBusy = true;

				using (var client = new HttpClient())
				{
					//grab json from server
					var json = await client.GetStringAsync("http://demo4404797.mockable.io/speakers");

					//Deserialize json
					var items = JsonConvert.DeserializeObject<List<Speaker>>(json);

					//Load speakers into list
					Speakers.Clear();
					foreach (var item in items)
						Speakers.Add(item);
				}

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
	}
}
