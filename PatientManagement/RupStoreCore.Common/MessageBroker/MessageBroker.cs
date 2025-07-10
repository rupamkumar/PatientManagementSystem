using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RupStoreCore.Common.MessageBroker
{
   public class MessageBroker
    {
        public delegate void MessageReceivedEventHandler(object sender, MessageBrokerEventArgs e);

        public event MessageReceivedEventHandler MessageReceived;

        private static MessageBroker _Instance;
        public static MessageBroker Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MessageBroker();
                }
                return _Instance;
            }
            set { _Instance = value; }
        }

        public void SendMessage(string messageName, object payload)
        {
            MessageBrokerEventArgs arg;
            arg = new MessageBrokerEventArgs(messageName, payload);

            RaiseMessageReceived(arg);
        }

        

        protected void RaiseMessageReceived(MessageBrokerEventArgs e)
        {
           if(MessageReceived != null)
            {
                MessageReceived(this, e);
            }
        }
    }
}
