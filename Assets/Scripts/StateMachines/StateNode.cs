using StateMachines.Interfaces;
using System.Collections.Generic;

namespace StateMachines
{
    public class StateNode<T> where T : IState
    {
        public T State { get; set; }
        public HashSet<ITransition<T>> Transitions { get; }

        public StateNode(T state)
        {
            State = state;
            Transitions = new HashSet<ITransition<T>>();
        }

        public void AddTransition(T addToState, IPredicate condition)
        {
            Transitions.Add(new Transition<T>(addToState, condition));
        }
    }
}
