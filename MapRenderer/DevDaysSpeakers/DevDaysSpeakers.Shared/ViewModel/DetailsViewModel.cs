using DevDaysSpeakers.Services;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevDaysSpeakers.ViewModel
{
	public class DetailsViewModel : BaseViewModel
	{
		private INavigationService _navigationService = null;
		private ISpeakersService _speakersService = null;


		public DetailsViewModel(INavigationService navigationService, ISpeakersService speakersService)
			: base()
		{
			_navigationService = navigationService;
			_speakersService = speakersService;
		}

		public async override void OnNavigatedTo(NavigationParameters parameters)
		{
			base.OnNavigatedTo(parameters);

			if (parameters == null || !parameters.ContainsKey("speakerid"))
			{
				ResetUi();
				return;
			}

			var speakerId = (string) parameters["speakerid"];
			var speaker = await _speakersService.GetSpeakerAsync(speakerId);

			if (speaker == null)
			{
				ResetUi();
			}
			else
			{
				Name = speaker.Name;
				Title = speaker.Title;
				Description = speaker.Description;
				Avatar = speaker.Avatar;
			}

		}

		public override void OnNavigatedFrom(NavigationParameters parameters)
		{
		}



		private void ResetUi()
		{
			Name = "Not found";
			Title = null;
			Description = "[no description]";
			Avatar = null;
		}





		private string _name = null;
		public string Name
		{
			get { return _name; }
			set { SetProperty<string>(ref _name, value); }
		}

		private string _title = null;
		public string Title
		{
			get { return _title; }
			set { SetProperty<string>(ref _title, value); }
		}

		private string _description = null;
		public string Description
		{
			get { return _description; }
			set { SetProperty<string>(ref _description, value); }
		}


		private string _avatar = null;
		public string Avatar
		{
			get { return _avatar; }
			set { SetProperty<string>(ref _avatar, value); }
		}
	}
}
