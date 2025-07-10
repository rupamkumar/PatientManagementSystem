using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RupStoreCore.Common.MessageBroker
{
  public  class MessageBrokerEventArgs : EventArgs
    {
        public MessageBrokerEventArgs() : base()
        {

        }

        public MessageBrokerEventArgs(string messageName, object payload): base()
        {
            MessageName = messageName;
            MessagePayload = payload;
        }
        public string MessageName { get; set; }
        public object MessagePayload { get; set; }
    }
}
