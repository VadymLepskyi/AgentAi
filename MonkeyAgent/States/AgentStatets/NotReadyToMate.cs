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
    class NotReadyToMate : IState<StateBasedMonkeyAgent>
    {
        private static NotReadyToMate _instance;

        Random rnd;

        public static NotReadyToMate Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NotReadyToMate();
                }
                return _instance;
            }
        }

        private NotReadyToMate()
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

            //ProcreationCountDown <=0
            if (obj.ProcreationCountDown <= 0)
            {
                obj.Fsm.ChangeState(Mature.Instance);
                return;
            }

            //Range to another agent < AIModfiers.maxMeleeAttackRange and other agent Strength + Health > Strength + Health
            List<IEntity> inRange = obj.ViewedEntities.FindAll(x => x.GetType() != typeof(StateBasedMonkeyAgent) && x is Agent && AIVector.Distance(obj.Position, x.Position) < AIModifiers.maxMeleeAttackRange && (((Agent)x).Strength + ((Agent)x).Health) > (obj.Strength + obj.Health));
            if (inRange.Count > 0)
            {
                obj.Fsm.ChangeState(Scared.Instance);
                return;
            }


            //A agent of another type with range < AIModifiers.maxMeleeAttackRange and other agent Strength + Health < Strength + Health
            inRange = obj.ViewedEntities.FindAll(x => x.GetType() != typeof(StateBasedMonkeyAgent) && x is Agent && AIVector.Distance(obj.Position, x.Position) < AIModifiers.maxMeleeAttackRange && (((Agent)x).Strength + ((Agent)x).Health) < (obj.Strength + obj.Health));
            if (inRange.Count > 0)
            {
                obj.NextAction = new Attack((Agent)inRange[0]);
                return;
            }


            obj.Fsm.ChangeState(new Exploring(ExploringGoal.Random));


        }

        public void Exit(StateBasedMonkeyAgent obj)
        {

        }
    }
}
