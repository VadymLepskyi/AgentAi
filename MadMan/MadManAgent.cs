using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;
using System.Diagnostics;
namespace MadMan
{
    internal class MadManAgent : Agent
    {
        private float reproduceCooldown = 100f; // 100 sekunder
        private bool isMature = false;
        private float MaxReproduceRange= 5.0f;
        private int speed = 500;

        Random rnd;

        //Only for randomization of movement
        float moveX = 0;
        float moveY = 0;

        public MadManAgent(IPropertyStorage propertyStorage)
             : base(propertyStorage)
        {
            rnd = new Random();
            MovementSpeed = 35;
            Strength = 35;
            Health = 70;
            Eyesight = 30;
            Endurance = 25;
            Dodge = 55;

            moveX = rnd.Next(-1, 2);
            moveY = rnd.Next(-1, 2);
        }

        public override IAction GetNextAction(List<IEntity> otherEntities)
        {
            // Opdater cooldowns (husk at justere deltaTime efter behov)
            UpdateCooldowns(1.0f); // Brug evt. en variabel til at angive den rigtige tid pr. frame

            List<Agent> agents = otherEntities.FindAll(a => a is Agent).ConvertAll<Agent>(a => (Agent)a);
            List<IEntity> plants = otherEntities.FindAll(a => a is Plant);

            // Formering hvis muligt
            List<Agent> mateCandidates = agents.FindAll(a => a.GetType() == typeof(MadManAgent)
                                                             && DistanceTo(a) <= MaxReproduceRange
                                                             && isMature
                                                             && reproduceCooldown <= 100
                                                             && ((MadManAgent)a).isMature
                                                             && ((MadManAgent)a).reproduceCooldown <=100);
            if (mateCandidates.Count > 0)
            {
                return new Procreate(mateCandidates[rnd.Next(mateCandidates.Count)]);
            }

            Agent rndAgent = null;
            if (agents.Count > 0)
            {
                rndAgent = agents[rnd.Next(agents.Count)];
            };

            switch (rnd.Next(5))
            {
                case 1: //Procreate
                    if (rndAgent != null && rndAgent.GetType() == typeof(MadManAgent))
                    {
                        return new Procreate(rndAgent);
                    }
                    break;

                case 2: //Attack Melee
                    if (rndAgent != null && rndAgent.GetType() != typeof(MadManAgent))
                    {
                        return new Attack(rndAgent);
                    }
                    break;
                case 3: //Feed
                    if (plants.Count > 0)
                    {
                        return new Feed((Plant)plants[rnd.Next(plants.Count)]);
                    }
                    break;
                case 4: //Move
                    return new Move(new AIVector(moveX, moveY));
                default:
                    return new Defend();
            }

            return new Move(new AIVector(moveX, moveY));

        }
        // Beregn afstanden til en anden enhed
        private float DistanceTo(IEntity other)
        {
            float deltaX = other.Position.X - this.Position.X;
            float deltaY = other.Position.Y - this.Position.Y;
            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
        public void UpdateCooldowns(float deltaTime)
        {
            if (reproduceCooldown > 100)
                reproduceCooldown -= deltaTime;
        }

        public override void ActionResultCallback(bool success)
        {
            //Do nothing - AI dont take success of an action into account
        }

    }
}

