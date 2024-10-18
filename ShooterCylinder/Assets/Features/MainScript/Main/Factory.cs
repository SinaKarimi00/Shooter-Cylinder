using System.Collections.Generic;
using Features.Enemy;
using Features.MainScript.Application;
using Features.Observer.Main.Listeners;
using Features.Player.Movement;
using Features.Player.Shooting;

namespace Features.MainScript.Main
{
    public class Factory
    {
        private readonly PlayerMovement _playerMovement;
        private readonly Shoot _shoot;
        private readonly EnemySpawner _enemySpawner;

        public Factory()
        {
            _playerMovement = new PlayerMovement();
            _shoot = new Shoot();
            _enemySpawner = new EnemySpawner();
        }

        public TimeCalculator CreateTimeCalculator()
        {
            return new TimeCalculator();
        }

        public List<IUpdater> CreateUpdaterClass()
        {
            var updaterClass = new List<IUpdater>
            {
                _playerMovement,
                _shoot,
                _enemySpawner
            };

            return updaterClass;
        }

        public List<IFixedUpdater> CreateFixedUpdaterClass()
        {
            var fixedUpdaterClass = new List<IFixedUpdater>
            {
                _playerMovement,
            };

            return fixedUpdaterClass;
        }
    }
}