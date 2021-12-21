using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.newScripts
{
    public class FSM
    {
        public Dictionary<(string, int), int> transitions = new Dictionary<(string, int), int>();
        public int currentState { get; private set; }
        public event EventHandler<int> onAction;
        public event EventHandler<int> onActionSilent;
        public void DoActionSilent(string action)
        {
            if(transitions.ContainsKey((action, currentState)))
            {
                currentState = transitions[(action, currentState)];
                onActionSilent?.Invoke(this, currentState);
            }
        }
        public void DoAction(string action)
        {
            DoActionSilent(action);
            onAction?.Invoke(this, currentState);
        }
        public FSM()
        { }
        public void Load(List<string> lines)
        {
            throw new NotImplementedException();
        }
        public void Set(int state)
        {
            currentState = state;
            onActionSilent?.Invoke(this, currentState);
        }
    }
}
