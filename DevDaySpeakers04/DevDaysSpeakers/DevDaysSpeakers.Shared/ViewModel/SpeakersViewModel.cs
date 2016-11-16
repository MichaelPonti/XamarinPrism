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
using Prism.Navigation;

namespace DevDaysSpeakers.ViewModel
{
	public class SpeakersViewModel : BaseViewModel
	{
		private Services.ISpeakersService _speakersService = null;
		private INavigationService _navigationService = null;

		public SpeakersViewModel(Services.ISpeakersService speakersService, INavigationService navigationService)
			: base()
		{
			_speakersService = speakersService;
			_navigationService = navigationService;
		}

		public ObservableCollection<Speaker> Speakers { get; set; } =
			new ObservableCollection<Speaker>();

		private Speaker _selectedSpeaker = null;
		public Speaker SelectedSpeaker
		{
			get { return _selectedSpeaker; }
			set
			{
				SetProperty<Speaker>(ref _selectedSpeaker, value);
				if (_selectedSpeaker != null)
				{
					NavigationParameters nps = new NavigationParameters();
					nps.Add("speakerid", _selectedSpeaker.Name);
					_navigationService.NavigateAsync(PageKeys.Details, nps);
				}
			}

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


		public override void OnNavigatedTo(NavigationParameters parameters)
		{
			base.OnNavigatedTo(parameters);
			if (parameters == null || !parameters.ContainsKey("first"))
				return;

			bool firstLoad = (bool) parameters["first"];
			if (firstLoad)
			{
				// do something
			}
			else
			{
				// do something else
			}
		}

		public override void OnNavigatedFrom(NavigationParameters parameters)
		{
			base.OnNavigatedFrom(parameters);
		}
	}
}
