using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class TransformMover: MonoBehaviour, IMover
    {
        protected float moveSpeed, turnSpeed;

        private bool _knockedBack;

        public void Init( float moveSpeed, float turnSpeed )
        {
            this.moveSpeed = moveSpeed;
            this.turnSpeed = turnSpeed;
        }

        public void Turn(float amount)
        {
            if (_knockedBack) return;
            Vector3 rotation = transform.localEulerAngles;
            rotation.y += amount * turnSpeed * Time.deltaTime;
            transform.localEulerAngles = rotation;
        }

        public void Move (float amount)
        {
            if (_knockedBack) return;

            Vector3 position = transform.position;
            Vector3 movement = transform.forward * amount * moveSpeed * Time.deltaTime;
            position += movement;
            transform.position = position;
        }

        public void Move( Vector3 direction )
        {
            if (_knockedBack) return;

            direction = direction.normalized;
            Vector3 position = transform.position + direction * moveSpeed * Time.deltaTime;
            transform.position = position;
        }

        public void Turn( Vector3 target )
        {
            if (_knockedBack) return;

            Vector3 direction = target - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        public IEnumerator Knockback(Vector3 direction)
        {
            _knockedBack = true;
            float knockbackTimer = 1f;
            float knockBackForce = Random.Range(3f, 5f);
            float rotDir = Mathf.RoundToInt(Random.value) == 0 ? 100 : -100;
            while (knockbackTimer > 0)
            {
                transform.position += direction * knockBackForce * Time.deltaTime;
                transform.Rotate(Vector3.up,  rotDir * knockBackForce * Time.deltaTime);
                knockBackForce -= Time.deltaTime;
                knockbackTimer -= Time.deltaTime;
                yield return null;
            }

            _knockedBack = false;
            yield return null;

        }
    }
}