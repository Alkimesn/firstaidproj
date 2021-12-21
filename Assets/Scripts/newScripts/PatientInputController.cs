using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.newScripts
{
    class PatientInputController:MonoBehaviour
    {
        public PatientOutputController master;
        public String partName;
        public void Activate(float dist, string action)
        {
            if (dist < 10)
            {
                master.DoAction(action + partName);
            }
        }
        public void Load(List<string> lines)
        {
            throw new NotImplementedException();
        }
    }
}
