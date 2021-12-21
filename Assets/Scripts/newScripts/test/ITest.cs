using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.newScripts.test
{
    interface ITest
    {
        void Start();
        float GetCurrentMark();
        bool Evaluate(int newState);
    }
}
