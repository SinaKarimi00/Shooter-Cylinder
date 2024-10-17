using System.Collections.Generic;
using Features.Main.Application;
using UnityEngine;

namespace Features.Main.Main
{
    public class Main : MonoBehaviour
    {
        private readonly List<IUpdater> _updateNeededElements = new List<IUpdater>();
        private readonly List<IFixedUpdater> _fixedUpdateNeededElements = new List<IFixedUpdater>();

        private void Awake()
        {
            var factory = new Factory();

            _updateNeededElements.AddRange(GetUpdateNeededElements());
            _fixedUpdateNeededElements.AddRange(GetFixedUpdateNeededElements());

            List<IUpdater> GetUpdateNeededElements() => factory.CreateUpdaterClass();
            List<IFixedUpdater> GetFixedUpdateNeededElements() => factory.CreateFixedUpdaterClass();
        }

        private void Update()
        {
            foreach (var updateNeededElement in _updateNeededElements)
                updateNeededElement.Update();
        }

        private void FixedUpdate()
        {
            foreach (var fixedUpdateNeededElement in _fixedUpdateNeededElements)
                fixedUpdateNeededElement.FixedUpdate();
        }
    }
}