using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameLoopState : IState
    {
        private IGameFactory _gameFactory;

        public GameLoopState(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            StartGameplay();
        }

        private void StartGameplay()
        {
            
        }

        public void Exit()
        {

        }
    }
}