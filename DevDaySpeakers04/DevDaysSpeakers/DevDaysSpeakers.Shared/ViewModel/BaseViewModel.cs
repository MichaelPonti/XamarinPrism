using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevDaysSpeakers.ViewModel
{
	public abstract class BaseViewModel : BindableBase, INavigationAware
	{
		public BaseViewModel()
			: base()
		{
		}


		private bool _isBusy = false;
		public bool IsBusy
		{
			get { return _isBusy; }
			set { SetProperty<bool>(ref _isBusy, value); }
		}

		public virtual void OnNavigatedFrom(NavigationParameters parameters)
		{
		}

		public virtual void OnNavigatedTo(NavigationParameters parameters)
		{
		}
	}
}
