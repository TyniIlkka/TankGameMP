using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class Projectile: MonoBehaviour
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

        private System.Action< Projectile > _collisionCallback;

        public float flyTime = 2;
        private float _flyTimer;

        public Rigidbody Rigidbody
        {
            get
            {
                if(_rigidbody == null)
                {
                    _rigidbody = gameObject.GetOrAddComponent<Rigidbody>();
                    _rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
                }
                return _rigidbody;
            }
        }

        public void Update()
        {
            if(_flyTimer > 0)
            {
                _flyTimer -= Time.deltaTime;
            } else
            {
                _flyTimer = flyTime;
                _collisionCallback(this);
            }
        }

        public void Init (System.Action< Projectile> collisionCallback)
        {
            _collisionCallback = collisionCallback;
        }

        public void Launch (Vector3 direction)
        {
            //TODO: Add particle effects.
            Rigidbody.AddForce(direction.normalized * _shootingForce, ForceMode.Impulse);
        }

        protected void OnCollisionEnter( Collision collision )
        {
            //TODO: Add particle effects.
            IDamageReceiver receiver = collision.gameObject.GetComponentInParent<IDamageReceiver>();
            if(receiver != null)
            {
                ApplyDamage(receiver);
            }
            Rigidbody.velocity = Vector3.zero;
            _collisionCallback(this);
        }

        private void ApplyDamage(IDamageReceiver damageReceiver)
        {
            damageReceiver.TakeDamage(_damage);
        }
        
    }
}
