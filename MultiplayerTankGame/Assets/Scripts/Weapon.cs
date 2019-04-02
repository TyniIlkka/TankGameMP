using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace TankGame
{
    public class Weapon : NetworkBehaviour
    {
        [SerializeField]
        private Projectile _projectilePrefab;

        [Tooltip("Ammo / Second")]
        [SerializeField]
        private float _firingRate = 1 / 3f;

        [SerializeField]
        private Transform _shootingPoint;

        private Tank _owner;
        public bool _canShoot = true;
        private float _firingTimer = 0;

        public void Init(Tank owner)
        {
            _owner = owner;
        }


        [Command]
        public void CmdShoot()
        {

            Projectile projectile = Instantiate(_projectilePrefab, _shootingPoint.position, Quaternion.identity);
            if (projectile != null)
            {
                NetworkServer.Spawn(projectile.gameObject);
                projectile.RpcLaunch(_shootingPoint.forward, _owner.gameObject);
            }
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