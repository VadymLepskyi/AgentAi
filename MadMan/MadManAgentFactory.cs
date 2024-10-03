using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIFramework;
using AIFramework.Entities;
using System.Diagnostics;
using System.Reflection;

namespace MadMan
{

        public class MadManAgentFactory : AgentFactory
        {
            public override Agent CreateAgent(IPropertyStorage propertyStorage)
            {
                return new MadManAgent(propertyStorage);
            }

            public override Agent CreateAgent(Agent parent1, Agent parent2, IPropertyStorage propertyStorage)
            {
                return new MadManAgent(propertyStorage);
            }

            public override Type ProvidedAgentType
            {
                get { return typeof(MadManAgent); }
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

