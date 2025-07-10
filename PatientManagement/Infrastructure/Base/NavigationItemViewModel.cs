using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Infrastructure.Base
{
  public  class NavigationItemViewModel : ViewModelBase
    {

        private readonly IEventAggregator _eventAggregator;
        private string _displayValue;

        public NavigationItemViewModel(Guid patientId, string displayValue, IEventAggregator eventAggregator)
        {
            PatientId = patientId;
            DisplayValue = displayValue;
            _eventAggregator = eventAggregator;
            //ObjectEditViewCommand = new DelegateCommand<object>(ObjectEditViewExcute);
            // _proxy = proxy;
        }

        public Guid PatientId { get; private set; }

        //private WcfProxy _proxy { get; set; }

        public string DisplayValue
        {
            get { return _displayValue; }
            set
            {
                _displayValue = value;
                OnPropertyChanged();
            }
        }

        public ICommand ObjectEditViewCommand { get; set; }

        //private void ObjectEditViewExcute(object obj )
        //{
        //    _eventAggregator.GetEvent<T>().Publish(PatientId);
        //}
    }
}
