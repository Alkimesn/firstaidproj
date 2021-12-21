using Assets.Scripts.newScripts.test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.newScripts
{
    class TestController
    {
        public WorldController worldController;
        ITest test;
        NetworkController networkController;
        TestLogger logger;

        void Init(NetworkController networkController)
        {
            this.networkController = networkController;
            networkController.onReceive += (s, e) =>
              {
                  if (e.containerType == ContainerType.FSMLIST_ACTION_DATA)
                  {
                      if (int.TryParse(e.data, out int res))
                      {
                          logger.Log(e.data);
                          bool isFinished = test.Evaluate(res);
                          networkController.Send(ContainerType.TEST_RESULT, TestResult.FromFloat(test.GetCurrentMark()));
                          logger.Log(test.GetCurrentMark().ToString());
                          if (isFinished)
                          {
                              networkController.Send(ContainerType.TEST_FINISHED, null);
                              logger.Log("Test finished");
                          }
                      }
                      else
                      {
                          logger.Log("Parse error");
                          logger.Log(e.data);
                      }
                  }
              };
        }

        void Start()
        {
            test.Start();
            networkController.Send(ContainerType.TEST_RESULT, TestResult.FromFloat(test.GetCurrentMark()));
        }
    }
}
