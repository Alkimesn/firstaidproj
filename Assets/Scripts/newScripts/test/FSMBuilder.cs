using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.newScripts.test
{
    static class FSMBuilder
    {
        public static FSM build(int fsmcode, int initialState)
        {
            FSM fsm = new FSM();
            if(fsmcode==0)
            {
                //start state 1, shifts to 2 with tool1, and 3 with tool2
                fsm.transitions.Add(("tool1head", 0), 1);
                fsm.transitions.Add(("tool2head", 0), 2);
                fsm.transitions.Add(("tool2body", 0), 2);
                fsm.Set(0);
            }
            return fsm;
        }
    }
}
