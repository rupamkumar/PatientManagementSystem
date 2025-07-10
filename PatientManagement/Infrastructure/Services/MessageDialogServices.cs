
using Infrastructure.ServiceInterface;
using System;
using System.ComponentModel.Composition;
using System.Windows;

namespace Infrastructure.Services
{
    [Export(typeof(IMessageDialogService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MessageDialogServices : IMessageDialogService 
    {
        public MessageDialogServices()
        {

        }
        public MessageDialogResult ShowYesNoDialog(Type window, string title, string text,
            MessageDialogResult defaultResult = MessageDialogResult.Yes)
        {
            var dlg =(Window)CreateInstance(window, title, text, defaultResult, MessageDialogResult.Yes, MessageDialogResult.No);
           
            dlg.Owner = Application.Current.MainWindow;            

            MessageDialogResult result = (MessageDialogResult)dlg.GetType().GetMethod("ShowDialog").Invoke(dlg, null);
            //(MessageDialogResult)dlg.GetType().GetProperty("Result").GetValue(dlg);
            return result;
        }

        protected object CreateInstance(Type type, params object[] args)
        {
           return  Activator.CreateInstance(type, args);
        }

    }
       
}
