using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.newScripts
{
    class TestResult : ISendable
    {
        public string pack()
        {
            throw new NotImplementedException();
        }

        public ISendable unpack(string data)
        {
            throw new NotImplementedException();
        }

        public static TestResult FromFloat(float input)
        {
            return null;
        }
    }
}
