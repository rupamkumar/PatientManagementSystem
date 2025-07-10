using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Prism.Regions;
using Moq;
using PatientManagement.PatientVault.Model;
using PatientManagement.PatientVault.ViewModels;


namespace PatientManagement.PatientVault.Tests
{
    [TestClass]
    public class PatientViewModelFixture
    {
        [TestMethod]
        public void WhenNavigateTo_ThenRequestPatientFromService()
        {
            var patient = new Patient();
            var patientServiceMock = new Mock<IPatientVaultService>();
            patientServiceMock
                .Setup(svc => svc.GetPatient(patient.Id))
                .Returns(patient)
                .Verifiable();

            var viewModel = new PatientDetailsViewModel(patientServiceMock.Object);

            var notified = false;
            viewModel.PropertyChanged += (s, o) => notified = o.PropertyName == "Patient";

            NavigationContext context = new NavigationContext(new Mock<IRegionNavigationService>().Object, new Uri("location", UriKind.Relative));
            context.Parameters.Add("PatientId", patient.Id);
            ((INavigationAware)viewModel).OnNavigatedTo(context);
            Assert.IsTrue(notified);
            patientServiceMock.VerifyAll();


        }
        
    }
}
