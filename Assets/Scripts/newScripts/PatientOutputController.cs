using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.newScripts
{
    public class PatientOutputController:MonoBehaviour
    {
        FSM fsm;
        int testcode;
        PatientInputController inputController;
        MeshRenderer renderer;
        public List<Material> materials;

        public void Start()
        {
            renderer = gameObject.GetComponent<MeshRenderer>();
        }
        public void Load(FSM fsm, int testcode)
        {
            this.fsm = fsm;
            this.testcode = testcode;
            this.fsm.onAction += (s, e) =>
              {
                  renderer.material = materials[e];
              };
        }
        public void DoAction(String action)
        {
            fsm.DoAction(action);
        }
    }
}
