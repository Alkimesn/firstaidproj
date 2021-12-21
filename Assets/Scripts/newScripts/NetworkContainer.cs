using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.newScripts
{
    class NetworkContainer
    {
        public string data;
        public ContainerType containerType;
        public static NetworkContainer fromObject(ContainerType type, ISendable obj)
        {
            NetworkContainer container = new NetworkContainer();
            container.data = obj.pack();
            container.containerType = type;
            return container;
        }
    }
}
