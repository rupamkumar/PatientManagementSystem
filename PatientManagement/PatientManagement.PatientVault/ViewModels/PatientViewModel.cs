using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using PatientManagement.Model;
using PatientManagement.PatientVault.DataProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Infrastructure.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PatientManagement.PatientVault.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PatientViewModel : ViewModelBase
    {
        private readonly IPatientDataProvider _patientDataProvider;
       // private IRegionNavigationJournal navigationJournal;
        //private Patient patient;
        private Patient _selectedPatient;
       
        
        [ImportingConstructor]
        public PatientViewModel(IPatientDataProvider patientDataProvider)
        {
            this._patientDataProvider = patientDataProvider;
            Patients = new ObservableCollection<Patient>();
        }

        public ObservableCollection<Patient> Patients { get; set; }

        public async Task LoadAsync()
        {
            var patients = await _patientDataProvider.GetAllPatientsAsync();

            Patients.Clear();
            foreach(var patient in patients)
            {
                Patients.Add(patient);
            }
           // return patients;
        }

       
        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                OnPropertyChanged();
            }
        }

        //public bool IsNavigationTarget(NavigationContext navigationContext)
        //{
        //    if(this.patient == null)
        //    {
        //        return true;
        //    }
        //    var requestedPatientId = GetRequestPatientId(navigationContext);
        //    return requestedPatientId.HasValue && requestedPatientId.Value == this.patient.PatientId;
        //}

        //public void OnNavigatedFrom(NavigationContext navigationContext)
        //{
        //    this.navigationJournal = navigationContext.NavigationService.Journal;
        //}

        //public void OnNavigatedTo(NavigationContext navigationContext)
        //{
        //    var patientId = GetRequestPatientId(navigationContext);
        //    if(patientId.HasValue)
        //    {
        //        this.Patient = this._patientDataProvider.GetPatientById(patientId);
        //    }
        //    this.navigationJournal = navigationContext.NavigationService.Journal;
        //}

        //private Guid? GetRequestPatientId(NavigationContext navigationContext)
        //{
        //    var patient = navigationContext.Parameters[PatientIdKey];
        //    Guid patientid;
        //    if(patient != null)
        //    {
        //        if(patient is Guid)
        //        {
        //            patientid = (Guid)patient;
        //        }
        //        else
        //        {
        //            patientid = Guid.Parse(patient.ToString());
        //        }
        //        return patientid;
        //    }
        //    return null;
        //}
    }
}
