using AIFramework.Actions;
using AIFramework;
using AIFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyAgent.States;
using MonkeyAgent.States.AgentStatets;

namespace MonkeyAgent
{
    public class StateBasedMonkeyAgent : Agent
    {
        FSM<StateBasedMonkeyAgent> fsm;

        public FSM<StateBasedMonkeyAgent> Fsm
        {
            get { return fsm; }
        }

        IAction nextAction;
        List<IEntity> viewedEntities;

        public List<IEntity> ViewedEntities
        {
            get { return viewedEntities; }
        }

        public IAction NextAction
        {
            get { return nextAction; }
            set { nextAction = value; }
        }


        public StateBasedMonkeyAgent(IPropertyStorage propertyStorage)
            : base(propertyStorage)

        {
            MovementSpeed = 70;
            Strength = 25;
            Health = 10;
            Eyesight = 35;
            Endurance = 55;
            Dodge = 55;


            //Start state created
            fsm = new FSM<StateBasedMonkeyAgent>(this);
            fsm.ChangeState(Born.Instance);

        }

        public override IAction GetNextAction(List<IEntity> otherEntities)
        {
            viewedEntities = otherEntities;

            nextAction = null;
            while (nextAction == null)
            {
                fsm.Update();
            }
            return nextAction;
        }

        public override void ActionResultCallback(bool success)
        {
            //throw new NotImplementedException();
        }
    }
}
