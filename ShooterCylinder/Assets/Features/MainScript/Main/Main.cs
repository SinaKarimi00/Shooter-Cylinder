using System.Collections.Generic;
using Features.DependencyInjection.Main;
using Features.MainScript.Application;
using Features.Observer.Application;
using Features.Observer.Main;
using UnityEngine;

namespace Features.MainScript.Main
{
    public class Main : MonoBehaviour, IListener
    {
        private readonly List<IUpdater> _updateNeededElements = new List<IUpdater>();
        private readonly List<IFixedUpdater> _fixedUpdateNeededElements = new List<IFixedUpdater>();
        private bool _isGameFinish;

        private void Awake()
        {
            var eventService = DependencyInjector.Instance.GetDependency<EventService>();
            eventService.Register<Main>(this);

            var factory = new Factory();

            _updateNeededElements.AddRange(GetUpdateNeededElements());
            _fixedUpdateNeededElements.AddRange(GetFixedUpdateNeededElements());
            factory.CreateTimeCalculator();
            Cursor.lockState = CursorLockMode.Locked;


            List<IUpdater> GetUpdateNeededElements() => factory.CreateUpdaterClass();
            List<IFixedUpdater> GetFixedUpdateNeededElements() => factory.CreateFixedUpdaterClass();
        }

        private void Update()
        {
            if (_isGameFinish) return;
            foreach (var updateNeededElement in _updateNeededElements)
                updateNeededElement.Update();
        }

        private void FixedUpdate()
        {
            if (_isGameFinish) return;
            foreach (var fixedUpdateNeededElement in _fixedUpdateNeededElements)
                fixedUpdateNeededElement.FixedUpdate();
        }

        public void ReactionToEvent(IEvent generatedEvent)
        {
            _isGameFinish = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}