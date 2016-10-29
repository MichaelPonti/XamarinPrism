using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevDaysSpeakers.ViewModel
{
	public abstract class BaseViewModel : BindableBase
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
	}
}
