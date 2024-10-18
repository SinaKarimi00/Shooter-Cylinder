using Features.DependencyInjection.Main;
using Features.MainScript.Main;
using Features.Observer.Main;
using Features.Observer.Main.Events;
using Features.Observer.Main.Listeners;
using UnityEngine;

namespace Features.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        private EventService _eventService;

        private void Awake()
        {
            _eventService = DependencyInjector.Instance.GetDependency<EventService>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag("Enemy"))
            {
                var endGameEvent = new EndGameEvent();
                _eventService.SendNotification<Main>(endGameEvent);
                _eventService.SendNotification<TimeCalculator>(endGameEvent);
            }
        }
    }
}