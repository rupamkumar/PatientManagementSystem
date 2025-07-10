using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ServiceInterface
{
  public  interface IMessageDialogService
    {      
        MessageDialogResult ShowYesNoDialog(Type obj, string title, string text, MessageDialogResult defaultResult = MessageDialogResult.Yes);
    }

    public interface IWinform
    {
       MessageDialogResult ShowDialog();
    }
}
