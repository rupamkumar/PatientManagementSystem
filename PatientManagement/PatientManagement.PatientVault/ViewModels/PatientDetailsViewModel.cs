using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Mvvm;
using System.Threading;
using Microsoft.Practices.Prism.Regions;
using System.Collections.ObjectModel;
using PatientManagement.PatientVault.Model;
using System.ComponentModel;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Data;
using Infrastructure;
using System.Windows.Input;

namespace PatientManagement.PatientVault.ViewModels
{
    [Export]
    //[PartCreationPolicy(CreationPolicy.NonShared)]
   public class PatientDetailsViewModel : BindableBase, INavigationAware
    {
        private readonly SynchronizationContext synchronizationContext = SynchronizationContext.Current ?? new SynchronizationContext();
        private readonly IRegionManager regionManager;
        private readonly ObservableCollection<Patient> patientCollection;
        //private readonly ICollectionView patientsView;
        private readonly DelegateCommand<Patient> openPatientCommand;
        private readonly IPatientVaultService _patientVaultService;
        private Patient patient;
        private IRegionNavigationJournal navigationJournal;

        private const string PatientViewKey = "PatientView";
        private const string PatientIdKey = "PatientId";

       // [ImportingConstructor]
        public PatientDetailsViewModel(IPatientVaultService patientVaultService)
        {
            this._patientVaultService = patientVaultService;
        }

        [ImportingConstructor]
        public PatientDetailsViewModel(IPatientVaultService patientVaultService, IRegionManager regionManager)
        {
            this.patientCollection = new ObservableCollection<Patient>();
            this.PatientsView = new ListCollectionView(this.patientCollection);
            this.openPatientCommand = new DelegateCommand<Patient>(this.OpentPatient);
            this.regionManager = regionManager;

            patientVaultService.BeginGetPatients((ar) =>
            {
                IEnumerable<Patient> newPatients = patientVaultService.EndGetPatients(ar);

                this.synchronizationContext.Post((state) =>
                {
                    foreach(var newPatient in newPatients)
                    {
                        this.patientCollection.Add(newPatient);
                    }
                }, null);
                //Patients = newPatients;
            }, null);
            
        }

        public ICommand OpenPatientCommand
        {
            get { return this.openPatientCommand; }
        }

        private void OpentPatient(Patient pt)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(PatientIdKey, pt.Id.ToString("N"));
            this.regionManager.RequestNavigate(RegionNames.MainContentRegion, new Uri(PatientViewKey + parameters, UriKind.Relative));
        }

        public Patient Patient
        {
            get { return this.patient; }
            set
            {
                this.SetProperty(ref this.patient, value);
            }
        }

        public ICollectionView PatientsView
        {
            get;
            private set;
        }
        public IEnumerable<Patient> Patients
        {
            get;
            private set;
        }

        private Guid? GetRequestedPatientId(NavigationContext navigationContext)
        {
            var patient = navigationContext.Parameters[PatientIdKey];
            Guid patientId;
            if(patient != null)
            {
                if(patient is Guid)
                {
                    patientId = (Guid)patient;
                }
                else
                {
                    patientId = Guid.Parse(patient.ToString());
                }
                return patientId;
            }
            return null;
        }

       bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
        {
            if(this.patient == null)
            {
                return true;
            }

            var requestedPatientId = GetRequestedPatientId(navigationContext);

            return requestedPatientId.HasValue && requestedPatientId.Value == this.patient.Id;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var patientId = GetRequestedPatientId(navigationContext);
            if (patientId.HasValue)
            {
                this.Patient = this._patientVaultService.GetPatient(patientId.Value);
            }
            this.navigationJournal = navigationContext.NavigationService.Journal;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //not implemented
        }
    }
}
