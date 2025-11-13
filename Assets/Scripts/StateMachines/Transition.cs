using StateMachines.Interfaces;

namespace StateMachines
{
    public class Transition<T> : ITransition<T> where T : IState
    {
        public T TargetState { get; }
        public IPredicate Condition { get; }

        public Transition(T targetState, IPredicate condition)
        {
            TargetState = targetState;
            Condition = condition;
        }
    }
}
