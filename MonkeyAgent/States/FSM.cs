﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyAgent.States
{
    public class FSM<T>
    {
        // Context klasse
      
            private T obj;

            private IState<T> currentState;


            public FSM(T obj)
            {
                this.obj = obj;
            }

            public void ChangeState(IState<T> newState)
            {
                if (currentState != null)
                {
                    currentState.Exit(obj);
                }

                currentState = newState;

                currentState.Enter(obj);
            }

            public void Update()
            {
                if (currentState != null)
                {
                    currentState.Execute(obj);
                }
            }


        }
    }

