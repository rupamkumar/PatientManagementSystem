using DataAccess.Contracts;
using System;
using System.ServiceModel;

namespace DataAccess.Contracts.ServiceInterface
{
    public interface IClinicalDataServiceClient : IPatientService, IDisposable
    {
        void Close();
        void Open();
    }
}
