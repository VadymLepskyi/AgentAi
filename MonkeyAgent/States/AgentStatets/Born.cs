using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonkeyAgent.States.AgentStatets
{
    class Born : IState<StateBasedMonkeyAgent>
    {
        private static Born _instance;

        Random rnd;

        public static Born Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Born();
                }
                return _instance;
            }
        }

        private Born()
        {
            rnd = new Random();
        }

        public void Enter(StateBasedMonkeyAgent obj)
        {

        }

        public void Execute(StateBasedMonkeyAgent obj)
        {
            obj.Fsm.ChangeState(Mature.Instance);
        }

        public void Exit(StateBasedMonkeyAgent obj)
        {

        }
    }
}
