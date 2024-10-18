using System;
using System.Collections;
using System.Collections.Generic;
using Features.DependencyInjection.Main;
using Features.MainScript.Main;
using Features.Observer.Main;
using Features.Observer.Main.Events;
using Features.Observer.Main.Listeners;
using Features.Popup.Application;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Popup.Main.Popups
{
    public class StartGamePopup : MonoBehaviour, IPopup
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button exitGameButton;
        [SerializeField] private GameObject playerContainer;
        [SerializeField] private GameObject enemyContainer;
        [SerializeField] private GameObject mainScript;
        [SerializeField] private GameObject player;

        private readonly Dictionary<GameState, Action> _gameStateActionPair = new();
        private GameState _currentState;
        private GameState _lastState;

        private void Awake()
        {
            startGameButton.onClick.AddListener(StartGame);
            exitGameButton.onClick.AddListener(UnityEngine.Application.Quit);
            _gameStateActionPair.Add(GameState.TryToStartGame, CheckMonoDependencyInjector);
            _gameStateActionPair.Add(GameState.MonoDependencyInjectorIsActive, ActiveMainScript);
            _gameStateActionPair.Add(GameState.MainScriptIsActive, ActivePlayer);
            _gameStateActionPair.Add(GameState.GameStarted, Close);
            _currentState = GameState.None;
            _lastState = _currentState;
            GameDataPref.ResetData();
        }

        private void Update()
        {
            if (_lastState == _currentState) return;

            _lastState = _currentState;
            _gameStateActionPair[_currentState]?.Invoke();
        }

        public void Setup()
        {
        }

        public void Close()
        {
            var eventService = DependencyInjector.Instance.GetDependency<EventService>();
            eventService.SendNotification<TimeCalculator>(new StartTimeEvent());
            Destroy(gameObject);
        }

        private void StartGame()
        {
            _currentState = GameState.TryToStartGame;
        }

        private void CheckMonoDependencyInjector()
        {
            StartCoroutine(WaitForMonoDependencyInjector());
        }

        private IEnumerator WaitForMonoDependencyInjector()
        {
            while (DependencyInjector.Instance == null)
            {
                yield return new WaitForEndOfFrame();
            }

            playerContainer.SetActive(true);
            enemyContainer.SetActive(true);
            _currentState = GameState.MonoDependencyInjectorIsActive;
        }

        private void ActiveMainScript()
        {
            mainScript.SetActive(true);
            _currentState = GameState.MainScriptIsActive;
        }

        private void ActivePlayer()
        {
            player.SetActive(true);
            _currentState = GameState.GameStarted;
        }
    }

    enum GameState
    {
        None,
        TryToStartGame,
        MonoDependencyInjectorIsActive,
        MainScriptIsActive,
        GameStarted
    }
}