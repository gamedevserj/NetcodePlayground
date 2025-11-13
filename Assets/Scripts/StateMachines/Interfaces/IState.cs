namespace StateMachines.Interfaces
{
    public interface IState
    {

        void OnEnter();
        void OnExit();
        void OnFixedUpdate();

    }
}