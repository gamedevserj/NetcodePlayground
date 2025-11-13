namespace StateMachines.Interfaces
{
    public interface IStateMachine<T> where T : IState
    {
        T CurrentState { get; }

        void ChangeState(T newState);
        void OnFixedUpdate();

    }
}
