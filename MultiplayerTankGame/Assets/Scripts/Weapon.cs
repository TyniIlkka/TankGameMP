using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private Projectile _projectilePrefab;

        [Tooltip("Ammo / Second")]
        [SerializeField]
        private float _firingRate = 1 / 3f;

        [SerializeField]
        private Transform _shootingPoint;

        private Tank _owner;
        private bool _canShoot = true;
        private float _firingTimer = 0;

        public void Init(Tank owner)
        {
            _owner = owner;
        }


        public bool Shoot()
        {
            if (!_canShoot)
            {
                return false;
            }

            Projectile projectile = Instantiate(_projectilePrefab, _shootingPoint.position, Quaternion.identity);
            if (projectile != null)
            {
                projectile.Launch(_shootingPoint.forward, _owner.Health);
                _canShoot = false;
            }

            return projectile != null;
        }

        protected virtual void Update()
        {
            UpdateFiringTimer();
        }

        private void UpdateFiringTimer()
        {
            if (!_canShoot)
            {
                _firingTimer += Time.deltaTime;
                if (_firingTimer >= _firingRate)
                {
                    _canShoot = true;
                    _firingTimer = 0;
                }
            }
        }

    }
}