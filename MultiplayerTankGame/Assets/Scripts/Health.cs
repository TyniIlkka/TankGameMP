using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace TankGame
{
    public class Health : MonoBehaviour, IDamageReceiver
    {
        [SerializeField]
        private int _currentHealth, _maxHealth = 100;
        [SerializeField] private HUD _hud;
        [SerializeField] private HitPointDisplay _hitPointDisplay;

        public bool _isLocalPlayer;

        private Tank _tank;

        void Start()
        {
            Initialize();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (!_isLocalPlayer)
                {
                    TakeDamage(20);
                }
            }
            
        }

        public void Initialize()
        {
            _tank = GetComponentInParent<Tank>();
            _currentHealth = _maxHealth;
            _hitPointDisplay.UpdateHealthBar(_maxHealth, _currentHealth);

            if (_isLocalPlayer)
            {
                _hud.UpdateHealthDisplay(_maxHealth, _currentHealth);
                _hitPointDisplay.GetComponent<Canvas>().enabled = false;
            }
        }

        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;

            Debug.Log(_currentHealth);
            if(_currentHealth <= 0)
            {
                Gamemanager.Instance.PlayerDied(_tank);
            }

            _hitPointDisplay.UpdateHealthBar(_maxHealth, _currentHealth);

            if (!_isLocalPlayer) return;

            _hud.UpdateHealthDisplay(_maxHealth, _currentHealth);

        }

    }
}
