using AIFramework.Actions;
using AIFramework.Entities;
using AIFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyAgent.States.AgentStatets
{
    class Mature : IState<StateBasedMonkeyAgent>
    {
        private static Mature _instance;

        Random rnd;

        public static Mature Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Mature();
                }
                return _instance;
            }
        }

        private Mature()
        {
            rnd = new Random();
        }

        public void Enter(StateBasedMonkeyAgent obj)
        {

        }

        public void Execute(StateBasedMonkeyAgent obj)
        {
            if (obj.Hunger >= AIModifiers.maxHungerBeforeHitpointsDamage)
            {
                obj.Fsm.ChangeState(Hungry.Instance);
                return;
            }

            //ProcreationCountDown > 0
            if (obj.ProcreationCountDown > 0)
            {
                obj.Fsm.ChangeState(NotReadyToMate.Instance);
                return;
            }

            //Range to another agent < AIModfiers.maxMeleeAttackRange and other agent Strength + Health > Strength + Health
            List<IEntity> inRange = obj.ViewedEntities.FindAll(x => x.GetType() != typeof(StateBasedMonkeyAgent) && x is Agent && AIVector.Distance(obj.Position, x.Position) < AIModifiers.maxMeleeAttackRange && (((Agent)x).Strength + ((Agent)x).Health) > (obj.Strength + obj.Health));
            if (inRange.Count > 0)
            {
                obj.Fsm.ChangeState(Scared.Instance);
                return;
            }



            //Another agent of same type with range < AIModifiers.maxProcreateRange and other agent ProcreationCountDown <= 0
            inRange = obj.ViewedEntities.FindAll(x => x is StateBasedMonkeyAgent && ((StateBasedMonkeyAgent)x).ProcreationCountDown <= 0 && AIVector.Distance(obj.Position, x.Position) < AIModifiers.maxProcreateRange);
            if (inRange.Count > 0)
            {
                obj.NextAction = new Procreate((Agent)inRange[0]);
                return;
            }

            //Default
            obj.Fsm.ChangeState(new Exploring(ExploringGoal.Friend));
        }

        public void Exit(StateBasedMonkeyAgent obj)
        {

        }
    }
}
