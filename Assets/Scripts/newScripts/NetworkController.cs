using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.newScripts
{
    class NetworkController
    {
        public void Connect(string connectionData)
        { }
        public void Send(ContainerType type, ISendable obj)
        {

        }
        public event EventHandler<NetworkContainer> onReceive;
    }
}
