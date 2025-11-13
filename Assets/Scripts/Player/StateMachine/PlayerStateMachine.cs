using StateMachines;

namespace NetcodePlayground.Player
{
    public class PlayerStateMachine : StateMachine<IPlayerState>
    {
        public PlayerStateMachine(PlayerController controller)
        {
            var idle = new IdleState(controller);
            var move = new MoveState(controller);
            var airborne = new AirborneState(controller);

            AddTransition(idle, move, new FuncPredicate(() => controller.IsMoving));
            AddTransition(move, idle, new FuncPredicate(() => !controller.IsMoving));
            AddTransition(airborne, idle, new FuncPredicate(() => controller.IsGrounded && !controller.IsMoving));
            AddTransition(airborne, move, new FuncPredicate(() => controller.IsGrounded && controller.IsMoving));

            AddAnyTransition(airborne, new FuncPredicate(() => !controller.IsGrounded));

            ChangeState(idle);
        }
    }
}
