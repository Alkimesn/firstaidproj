using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.newScripts;
using Assets.Scripts.newScripts.test;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{
    public PatientOutputController patient;
    public PlayerHeadController player;
    List<PatientInputController> parts;
    public Text testText;

    void Start()
    {
        string res = player.ConnectToServer("init");

        FSM fsm;
        if (res=="0")
        {
            fsm = FSMBuilder.build(0, 0);
            BinaryTest binaryTest = new BinaryTest();
            binaryTest.positiveFinal = 1;
            binaryTest.negativeFinal = 2;
            fsm.onAction += (s, e) =>
              {
                  bool fin = binaryTest.Evaluate(e);
                  if (player.ConnectToServer("check") != "")
                  {
                      if (fin)
                          testText.text = "Result: " + binaryTest.GetCurrentMark();

                  }
                  else testText.text = "No connection";
              };
            patient.Load(fsm,0);
            player.tools = new List<string>() { "tool1", "tool2" };
        }
        if(res=="")
        {
            Application.Quit();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

}
