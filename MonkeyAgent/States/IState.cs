using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyAgent.States
{
    public interface IState<T>
    {
        // Interface, der definerer tilstandens adfærd
            void Enter(T obj);
            void Execute(T obj);
            void Exit(T obj);
        
    }
}
