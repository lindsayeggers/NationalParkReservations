using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DeliveryProviders
{
    public interface IDeliveryService
    {
        // Classes must implement this method, they can do it
        // however they wish
        void Send(string recipient, string message);

    }
}