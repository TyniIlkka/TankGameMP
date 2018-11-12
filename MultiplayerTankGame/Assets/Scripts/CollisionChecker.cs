using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class CollisionChecker : MonoBehaviour
    {

        private TransformMover _mover;

        // Use this for initialization
        void Start()
        {
            _mover = GetComponentInParent<TransformMover>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Tank"))
            {
                Debug.Log("asd");
                Vector3 direction = Vector3.Normalize(transform.position - collision.transform.position);
                direction.y = 0;
                _mover.StartCoroutine(_mover.Knockback(direction));
            }
        }
    }
}