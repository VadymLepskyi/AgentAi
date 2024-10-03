using AIFramework.Entities;
using AIFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyAgent
{
    internal class StateBbasedMonkeyFactory: AgentFactory
    {
        public override Agent CreateAgent(IPropertyStorage propertyStorage)
        {
            return new StateBasedMonkeyAgent(propertyStorage);
        }

        public override Agent CreateAgent(Agent parent1, Agent parent2, IPropertyStorage propertyStorage)
        {
            return new StateBasedMonkeyAgent(propertyStorage);
        }

        public override Type ProvidedAgentType
        {
            get { return typeof(StateBasedMonkeyAgent); }
        }

        public override string Creators
        {
            get { return "Vadym"; }
        }

        public override void RegisterWinners(List<Agent> sortedAfterDeathTime)
        {
            //Do data collection - Perhaps used to evolutionary algoritmen
        }
    }
}

