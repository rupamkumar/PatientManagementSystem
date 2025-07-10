using Microsoft.Practices.Unity;
using RupStoreAccessToken;
using RupStoreAuthentication.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RupStoreAuthentication.Views
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
   
    public partial class LoginControl : UserControl
    {
        public LoginControl()
        {
            InitializeComponent();
            //DataContext = ViewModel;
           // _viewModel = (LoginViewModel).Resources["viewModel"];
            //ViewModel.UserEntity.Password = txtPassword.Password;

        }
        private LoginViewModel _viewModel = new LoginViewModel();

        
        //[Dependency]
        //public LoginViewModel ViewModel
        //{
        //    get{
                
        //        return this.DataContext as LoginViewModel;
        //    }
        //    set{
        //        this.DataContext = value;
                
        //    }
        //}

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            
            // Add the Password manually because data binding does not work
            _viewModel.UserEntity.Password = txtPassword.Password;
            _viewModel.UserEntity.UserName = txtUserName.Text;


            this.Dispatcher.Invoke(() =>
            {
                _viewModel = new LoginViewModel(_viewModel.UserEntity);
                _viewModel.Login();
            });

        }
    }
}
