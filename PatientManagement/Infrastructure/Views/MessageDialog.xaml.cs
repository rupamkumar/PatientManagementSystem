using System;
using System.Collections.Generic;
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

namespace Infrastructure.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MessageDialog : Window
    {
        //private MessageDialogResult _result;
        public MessageDialog()
        {
            InitializeComponent();
        }

        //public MessageDialog(string title, string text, MessageDialogResult defaultResult, params MessageDialogResult[] buttons)
        //{
        //    InitializeComponent();
        //    Title = title;
        //    textBlock.Text = text;
        //    //InitializeButtons(buttons);
        //    _result = defaultResult;
        //}

        //public new MessageDialogResult ShowDialog()
        //{
        //    base.ShowDialog();
        //    return _result;
        //}
    }
}
