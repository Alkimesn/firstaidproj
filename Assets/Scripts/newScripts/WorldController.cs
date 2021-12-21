using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.newScripts
{
    class WorldController
    {
        Dictionary<int, PatientInputController> patients = new Dictionary<int, PatientInputController>();
        public NetworkController networkController;
        public void Load(WorldInitData data)
        {
            //creates patients, their controllers, uses their Load methods, subscribes to their FSM's OnAction, and subscribes to networkController
        }
    }
}
