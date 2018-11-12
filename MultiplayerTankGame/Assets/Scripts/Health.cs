using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TankGame
{
    public class Health : MonoBehaviour, IDamageReceiver
    {
        private int _currentHealth;

        void IDamageReceiver.TakeDamage(int amount)
        {
            _currentHealth -= amount;
        }
    }
}
