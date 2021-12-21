using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.newScripts
{
    class ServerNetworkController
    {
        public void Send(Client client, ContainerType type, ISendable obj)
        {

        }
        public void Start()
        { }
        public event EventHandler<(Client, NetworkContainer)> onReceive;
        public event EventHandler<Client> onClientConnection;
    }
}
