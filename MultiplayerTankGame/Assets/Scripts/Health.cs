using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TankGame
{
    public class Health : MonoBehaviour, IDamageReceiver
    {
        [SerializeField]
        private int _currentHealth, _maxHealth = 100;


        void Start()
        {
            _currentHealth = _maxHealth;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                TakeDamage(20);
            }
            
        }

        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            if(_currentHealth <= 0)
            {
                Gamemanager.Instance.PlayerDied();
            }
        }
    }
}
