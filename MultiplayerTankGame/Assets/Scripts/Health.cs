using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TankGame
{
    public class Health : MonoBehaviour, IDamageReceiver
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private HUD _hud;

        private float _currentHealth;

        void Start()
        {
            _currentHealth = _maxHealth;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                TakeDamage(10);
            }
        }

        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;

            if(_hud != null)
            {
                _hud.UpdateHealthDisplay(_maxHealth, _currentHealth);
            }
        }
    }
}
