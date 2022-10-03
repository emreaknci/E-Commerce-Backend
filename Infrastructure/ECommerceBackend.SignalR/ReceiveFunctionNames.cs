using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceBackend.SignalR
{
    public static class ReceiveFunctionNames
    {
        public const string ProductAddedMessage = "receiveProductAddedMessage";
        public const string OrderCreatedMessage = "receiveOrderCreatedMessage";
    }
}
