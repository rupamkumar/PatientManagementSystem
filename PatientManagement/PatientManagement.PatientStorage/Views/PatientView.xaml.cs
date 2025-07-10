
using PatientManagement.PatientStorage.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DataAccess.Contracts;

namespace PatientManagement.PatientStorage.Views
{
    /// <summary>
    /// Interaction logic for PatientView.xaml
    /// </summary>
    [Export(typeof(PatientView))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class PatientView : UserControl
    {

        public PatientView()
        {
            InitializeComponent();
            Loaded += PatientData_Loaded;
        }       
       
        
        private void PatientData_Loaded(object sender, RoutedEventArgs e)
        {
            RegisterClient(ViewModel.ClientName);
            ViewModel.Load();
        }

        public void RegisterClient(string clientName)
        {
            ViewModel.RegisterClient(clientName,this.HandleBroadcast);
        }

        private delegate void HandleBroadcastCallback(object sender, EventArgs e);
        public void HandleBroadcast(object sender, EventArgs e)
        {
            try
            {
                var eventData = (EventDataType)sender;
                if (!string.IsNullOrEmpty(eventData.EventMessage))
                {
                    Dispatcher.Invoke(() =>
                    {
                        this.txtEventMessages.Text = (string.Format("{0} (from {1})",
                            eventData.EventMessage, eventData.ClientName));
                    });
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        this.txtEventMessages.Text = string.Empty;
                    });
                    
                }
            }
            catch (Exception ex)
            {                
            }
        }       

        [Import]
        public PatientViewModel ViewModel
        {
            get { return this.DataContext as PatientViewModel; }
            set { this.DataContext = value; }
        }
    }
}
