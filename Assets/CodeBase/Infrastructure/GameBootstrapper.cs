using System.Collections;
using System.Collections.Generic;
using CodeBase.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameBootstrapper : MonoBehaviour
{
    private GameStateMachine _stateMachine;
    private IGameFactory _gameFactory;
    [Inject]
    void Construct(GameStateMachine stateMachine, IGameFactory gameFactory)
    {
        _stateMachine = stateMachine;
        _gameFactory = gameFactory;
    }

    private void Awake()
    {
        _gameFactory.Cleanup();
        _stateMachine.Enter<LoadLevelState>();

        DontDestroyOnLoad(this);

    }
}