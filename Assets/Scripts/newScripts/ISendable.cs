using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.newScripts
{
    interface ISendable
    {
        string pack();
        ISendable unpack(string data);
    }
}
