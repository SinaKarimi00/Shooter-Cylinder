using System;
using Features.ConfigProvider;
using Features.DependencyInjection.Main;
using Features.MainScript.Main;
using Features.Observer.Application;
using Features.Observer.Main.Events;
using Features.Player;
using Features.Popup.Main;
using Features.Popup.Main.Popups;
using UnityEngine;

namespace Features.Observer.Main.Listeners
{
    public class TimeCalculator : IListener
    {
        private readonly PlayerContainer _playerContainer;

        public TimeCalculator()
        {
            var configProviderService = DependencyInjector.Instance.GetDependency<IConfigProviderService>();
            var eventService = DependencyInjector.Instance.GetDependency<EventService>();
            _playerContainer = configProviderService.GetConfig<PlayerContainer>();
            eventService.Register<TimeCalculator>(this);
        }

        public void ReactionToEvent(IEvent generatedEvent)
        {
            switch (generatedEvent)
            {
                case StartTimeEvent:
                    SaveStartTime();
                    break;
                case EndGameEvent:
                    FinishTheGame();
                    break;
                default:
                    break;
            }
        }

        private void FinishTheGame()
        {
            var startTime = GameDataPref.GamePlayStartTime;
            var gamePlayTime = (DateTime.Now - startTime).TotalMinutes;
            GameDataPref.GamePlayTime = (float)gamePlayTime;
            var endGamePopupPrefab = Resources.Load<EndGamePopup>("Popups/EndGamePopup");
            var endGamePopup = PopupManager.ShowPopup<EndGamePopup>(endGamePopupPrefab, _playerContainer.PopupCanvas);
            endGamePopup.Setup();
        }

        private void SaveStartTime()
        {
            GameDataPref.GamePlayStartTime = DateTime.Now;
        }
    }
}