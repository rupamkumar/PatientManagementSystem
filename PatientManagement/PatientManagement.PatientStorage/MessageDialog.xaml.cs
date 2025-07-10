using Infrastructure;
using Infrastructure.ServiceInterface;
using MahApps.Metro.Controls;
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

namespace PatientManagement.PatientStorage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    //[Export]
    public partial class MessageDialog : MetroWindow, IWinform
    {
        private MessageDialogResult _result;
        public MessageDialog(string title, string text, MessageDialogResult defaultResult, params MessageDialogResult[] buttons)
        {
            InitializeComponent();
            Title = title;
            textBlock.Text = text;
            InitializeButtons(buttons);
            _result = defaultResult;
        }
        private void InitializeButtons(MessageDialogResult[] buttons)
        {
            if (buttons == null || buttons.Length == 0)
            {
                buttons = new[] { MessageDialogResult.Ok };
            }
            foreach (var button in buttons)
            {
                var btn = new Button { Content = button, Tag = button };
                ButtonsPanel.Children.Add(btn);
                btn.Click += ButtonClick;
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var button = e.Source as Button;
            if(button != null)
            {
                _result = (MessageDialogResult)button.Tag;
                this.Close();
            }

        }

        public new MessageDialogResult ShowDialog()
        {
            base.ShowDialog();
            return _result;
        }

       
    }

    
}
