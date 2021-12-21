using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.newScripts
{
    class TestLog:ISendable
    {
        string userData;
        string testData;
        string log;

        public string pack()
        {
            throw new NotImplementedException();
        }

        public ISendable unpack(string data)
        {
            throw new NotImplementedException();
        }
    }
}
