using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class LoadLevelState : IState
    {
        private const string spawnPointTag = "HeroSpawnPoint";
        private GameStateMachine _gameStateMachine;
        private IGameFactory _gameFactory;
        private Vector3 _spawnPoint;
        
        public LoadLevelState(GameStateMachine gameStateMachine, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
        }
        
        public void Enter()
        {
            SceneManager.LoadSceneAsync("Main").completed += OnLoaded;
        }
        
        private void OnLoaded(AsyncOperation operation)
        {
            InitGameWorld();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            _gameFactory.SpawnUI();
            _gameFactory.SpawnMap(Vector3.zero);

            var spawnPointObject = GameObject.FindWithTag(spawnPointTag);
            if (spawnPointObject != null)
            {
                _spawnPoint = spawnPointObject.transform.position;
            }
            else
            {
                _spawnPoint = Vector3.zero;
            }

            _gameFactory.SpawnHero(_spawnPoint);
        }

        public void Exit()
        {
           
        }
    }
}