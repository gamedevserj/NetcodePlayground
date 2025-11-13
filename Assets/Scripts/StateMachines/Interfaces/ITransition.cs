namespace StateMachines.Interfaces
{
    public interface ITransition<T> where T : IState
    {
        T TargetState { get; }
        IPredicate Condition { get; }
    }
}
