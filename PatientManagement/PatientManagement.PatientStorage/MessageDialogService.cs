
using Infrastructure;
using Infrastructure.ServiceInterface;
using System;
using System.ComponentModel.Composition;
using System.Windows;

namespace PatientManagement.PatientStorage
{
    [Export(typeof(IMessageDialogService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MessageDialogService : IMessageDialogService 
    {
        public MessageDialogService()
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
