
using DataAccess.Contracts;
using DataAccess.Contracts.ServiceInterface;
using DataAccess.Proxies.ServiceProxies;
using Infrastructure.DataProvider;
using RupStoreCore.Common.Core;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure.Base
{
    public class ViewModelBase  : ObjectBase, INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;
             

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void WithClient<TClient>(TClient client,  Action<TClient> codeToExecute)
        {
            codeToExecute.Invoke(client);

            IDisposable disposableClient = client as IDisposable;
            //IDisposable disposableProxy = proxy as IDisposable;
            if(disposableClient != null)
            {
                disposableClient.Dispose();
            }
            
        }

       
        private IClinicalDataServiceClient proxy;
        private IBroadcastorServiceClient broadcastproxy;
        protected void ProxySetup(string clientName, EventHandler eventHandler, out IWcfProxy _proxy)
        {
            BroadcastorCallback cb = new BroadcastorCallback();
            cb.SetHandler(eventHandler);

            System.ServiceModel.InstanceContext context =
                new System.ServiceModel.InstanceContext(cb);

            _proxy = new WcfProxy();

            _proxy.ClientName = clientName;
            _proxy.BroadCastHandler = eventHandler;
            _proxy.Context = context;
        }

        protected void RegisterBroadcastorClient(string clientName, EventHandler eventHandler, out IWcfProxy _proxy)
        {
            BroadcastorCallback cb = new BroadcastorCallback();
            cb.SetHandler(eventHandler);

            System.ServiceModel.InstanceContext context =
                new System.ServiceModel.InstanceContext(cb);

            _proxy = new WcfProxy();
            broadcastproxy = new BroadcastServiceClient(context);
            broadcastproxy.RegisterClient(clientName);

            _proxy.ClientName = clientName;
            _proxy.BroadCastHandler = eventHandler;
            _proxy.Context = context;            
            _proxy.BroadcastorProxy = broadcastproxy;
        }

        private bool _disposed;
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if(proxy != null)
                {
                    proxy.Dispose();
                    proxy = null;
                }
                if(broadcastproxy != null)
                {
                    broadcastproxy.Dispose();
                    broadcastproxy = null;
                }
                _disposed = true;
            }
        }

        protected void CallUnmanagedPointer(IntPtr _unmanagedPointer)
        {
            if (_unmanagedPointer == IntPtr.Zero)
            {
                _unmanagedPointer = Marshal.AllocHGlobal(100 * 1024 * 1024);
            }
        }
        
    }
}
