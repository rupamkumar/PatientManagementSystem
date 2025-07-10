using Infrastructure.Base;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using RupStoreAccessToken;
using RupStoreAuthentication.Contracts;
using RupStoreAuthentication.Model;
using RupStoreCore.Common.MessageBroker;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace RupStoreAuthentication.ViewModel
{
       

    public class LoginViewModel : ViewModelBase, ILoginViewModel
    {


        IAccessTokenService _accessToken;
        //IConfiguration _config;
        

        public LoginViewModel()
        {            
            UserEntity = new User();
            //SetupCommand();
        }

        //public IConfiguration Accessconfiguration()
        //{
        //    return _config;
        //}

        public LoginViewModel( User User_Entity)
            //:this(new AccessTokenService())
        {
            _accessToken = new AccessTokenService();
            
            UserEntity = User_Entity;
            //SetupCommand();
        }
        private User _userentity;
        public User UserEntity {
            get
            {
             return _userentity;
            }
            set
            {
                _userentity = value;
                OnPropertyChanged();
                //if (OnLoginCanExecute())
                //{
                //    InvalidateCommands();
                //}
                
            }
        }
        //public string Password { get;  set; }

        public ICommand LoginCommand { get; private set; }
        //public void SetupCommand()
        //{            
        //    LoginCommand = new DelegateCommand();
        //}

        public void Login()
        {            
            OnLoginExecute();
        }

        private bool OnLoginCanExecute()
        {
            
            if (String.IsNullOrEmpty(UserEntity.UserName) || string.IsNullOrEmpty(UserEntity.Password))
                return false;
            else
                return true;
            
        }

        //private void InvalidateCommands()
        //{
        //    ((DelegateCommand)LoginCommand).RaiseCanExecuteChanged();
        //}


        
        private async void  OnLoginExecute()
        {
            //if(OnLoginCanExecute())
            //{
            var _access = RupStoreAccessSettings.Instance;
            _access.LoadSettings();
            try
            {
               
                var token = await _accessToken.GetToken(UserEntity.UserName, UserEntity.Password, _access.App().GrantType, _access.App().ClientId, _access.App().ClientSecret);
                AccessToken result = token;
                if (result.access_token == null)
                {
                    MessageBroker.Instance.SendMessage(MessageBrokerMessages.LOGIN_FAIL, UserEntity);
                }
                else
                {
                    MessageBroker.Instance.SendMessage(MessageBrokerMessages.LOGIN_SUCCESS, UserEntity);
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {

            }
        }

        private bool ValidateCredentials()
        {
            return true;
        }
    }
}
