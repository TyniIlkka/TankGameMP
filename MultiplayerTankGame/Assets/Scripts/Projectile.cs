using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace TankGame
{
    public class Projectile : NetworkBehaviour
    {
        [SerializeField]
        private int _damage;

        [SerializeField]
        private float _shootingForce;

        [SerializeField]
        private float _explosionForce;

        [SerializeField]
        private float explosionRadius;

        [SerializeField, HideInInspector]
        private int _hitMask;

        private Weapon _weapon;
        private Rigidbody _rigidbody;

        public float flyTime = 2;
        private float _flyTimer;

        private bool _initialized;

        private IDamageReceiver self;

        public Rigidbody Rigidbody
        {
            get
            {
                if (_rigidbody == null)
                {
                    _rigidbody = gameObject.GetOrAddComponent<Rigidbody>();
                    _rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
                }
                return _rigidbody;
            }
        }

        public void Update()
        {
            if (!_initialized) return;

            if (_flyTimer > 0)
            {
                _flyTimer -= Time.deltaTime;
            }
            else
            {
                NetworkServer.UnSpawn(this.gameObject);
                Destroy(gameObject);
            }
        }

        [ClientRpc]
        public void RpcLaunch(Vector3 direction, GameObject shooter)
        {
            //TODO: Add particle effects.
            _flyTimer = flyTime;
            self = shooter.GetComponent<IDamageReceiver>();
            GetComponent<SphereCollider>().enabled = true;
            Rigidbody.AddForce(direction.normalized * _shootingForce, ForceMode.Impulse);

            if(isServer)
                _initialized = true;
        }

        protected void OnTriggerEnter(Collider collision)
        {
            if (self == null || !isServer) return;

            IDamageReceiver receiver = collision.gameObject.GetComponentInParent<IDamageReceiver>();
            if (receiver == self) return;

            if (receiver != null)
            {
                ApplyDamage(receiver);
            }

            NetworkServer.UnSpawn(this.gameObject);
            Destroy(gameObject);
        }

        private void ApplyDamage(IDamageReceiver damageReceiver)
        {
            damageReceiver.TakeDamage(_damage);
        }

    }
}