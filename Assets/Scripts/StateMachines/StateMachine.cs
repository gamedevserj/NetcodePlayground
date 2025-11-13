using StateMachines.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachines
{
    public class StateMachine<T> : IStateMachine<T> where T : IState
    {

        private T _initialState;

        public T CurrentState => CurrentNode.State;
        private StateNode<T> CurrentNode { get; set; }
        private readonly Dictionary<T, StateNode<T>> _nodes = new();
        private readonly HashSet<ITransition<T>> _anyTransitions = new();

        public void OnFixedUpdate()
        {
            var transition = GetTransition();
            if (transition != null)
            {
                ChangeState(transition.TargetState);
            }
            CurrentNode.State.OnFixedUpdate();
        }

        public void SetInitialState(T initialState)
        {
            _initialState = initialState;
        }

        public void Reset()
        {
            ChangeState(_initialState);
        }

        public void ChangeState(T newState)
        {
            if (CurrentNode != null && EqualityComparer<T>.Default.Equals(CurrentNode.State, newState))
                return;

            CurrentNode?.State?.OnExit();
            CurrentNode = _nodes[newState];
            CurrentNode?.State?.OnEnter();
        }

        public void AddTransition(T from, T to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        public void AddAnyTransition(T to, IPredicate condition)
        {
            _anyTransitions.Add(new Transition<T>(GetOrAddNode(to).State, condition));
        }

        private ITransition<T> GetTransition()
        {
            foreach (var transition in _anyTransitions)
                if (transition.Condition.Evaluate())
                    return transition;

            foreach (var transition in CurrentNode.Transitions)
                if (transition.Condition.Evaluate())
                    return transition;

            return null;
        }

        private StateNode<T> GetOrAddNode(T state)
        {
            var node = _nodes.GetValueOrDefault(state);

            if (node == null)
            {
                node = new StateNode<T>(state);
                _nodes.Add(state, node);
            }
            return node;
        }
    }
}
