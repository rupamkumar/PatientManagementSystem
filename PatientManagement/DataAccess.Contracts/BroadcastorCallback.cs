﻿using DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
   public class BroadcastorCallback : IBroadcastorCallback
    {

        private System.Threading.SynchronizationContext syncContext =
           AsyncOperationManager.SynchronizationContext;

        private EventHandler _broadcastorCallBackHandler;
        public void SetHandler(EventHandler handler)
        {
            this._broadcastorCallBackHandler = handler;
        }

        public void BroadcastToClient(EventDataType eventData)
        {
            syncContext.Post(new System.Threading.SendOrPostCallback(OnBroadcast),
                   eventData);
        }

        private void OnBroadcast(object eventData)
        {

            this._broadcastorCallBackHandler.Invoke(eventData, null);
        }

    }
}
