using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace TankGame
{
    public class Health : NetworkBehaviour, IDamageReceiver
    {
        [SerializeField]
        private int _maxHealth = 100;
        [SerializeField] private HUD _hud;
        [SerializeField] private HitPointDisplay _hitPointDisplay;

        [SyncVar(hook = "OnChangeHealth")]
        private int _currentHealth;

        private Tank _tank;

        public override void OnStartClient()
        {
            if (isServer)
                _currentHealth = _maxHealth;

            _tank = GetComponentInParent<Tank>();
            OnChangeHealth(_currentHealth);
        }

        public override void OnStartLocalPlayer()
        {
            _hud = GetComponentInChildren<HUD>();
            _hud.UpdateHealthDisplay(_maxHealth, _currentHealth);
            _hitPointDisplay.GetComponent<Canvas>().enabled = false;
        }

        void Update()
        {
            if(isLocalPlayer && Input.GetKeyDown(KeyCode.P))
            {
                TakeDamage(20);
            }
        }

        void OnChangeHealth(int health)
        {
            _currentHealth = health;

            if(health <= 0)
            {
                Gamemanager.Instance.PlayerDied(_tank);
            }

            _hitPointDisplay.UpdateHealthBar(_maxHealth, health);

            if (isLocalPlayer && _hud)
                _hud.UpdateHealthDisplay(_maxHealth, health);
        }
        
        public void TakeDamage(int amount)
        {
            CmdTakeDamage(amount);
        }

        [Command]
        private void CmdTakeDamage(int amout)
        {
            _currentHealth -= amout;
        }

    }
}
