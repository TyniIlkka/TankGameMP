using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class Projectile : MonoBehaviour
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
                Destroy(gameObject);
            }
        }

        public void Launch(Vector3 direction, IDamageReceiver shooter)
        {
            //TODO: Add particle effects.
            _flyTimer = flyTime;
            self = shooter;
            GetComponent<SphereCollider>().enabled = true;
            Rigidbody.AddForce(direction.normalized * _shootingForce, ForceMode.Impulse);
            _initialized = true;
        }

        protected void OnTriggerEnter(Collider collision)
        {
            if (self == null) return;

            IDamageReceiver receiver = collision.gameObject.GetComponentInParent<IDamageReceiver>();
            if (receiver == self) return;

            if (receiver != null)
            {
                ApplyDamage(receiver);
            }

            Destroy(gameObject);
        }

        private void ApplyDamage(IDamageReceiver damageReceiver)
        {
            damageReceiver.TakeDamage(_damage);
        }

    }
}