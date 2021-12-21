using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.newScripts.teach
{
    class ChatMessage:ISendable
    {
        string time;
        string data;
        ChatType type;
        string sender;

        public string pack()
        {
            throw new NotImplementedException();
        }

        public ISendable unpack(string data)
        {
            throw new NotImplementedException();
        }

        ChatLog ToLog()
        { return null; }
    }
}
