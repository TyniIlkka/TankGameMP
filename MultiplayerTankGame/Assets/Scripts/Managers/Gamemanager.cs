using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class Gamemanager : MonoBehaviour
    {
        private static Gamemanager _instance;
        
        public static Gamemanager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject gameManager = new GameObject(typeof(Gamemanager).Name);
                    _instance = gameManager.AddComponent<Gamemanager>();
                }
                return _instance;
            }
        }

        public Tank player;

        [SerializeField]
        private float _deathTime = 3f;

        public void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        public void PlayerDied(Tank tank)
        {
            tank.gameObject.SetActive(false);
            StartCoroutine(RespawnTimer(_deathTime, tank));
        }

        IEnumerator RespawnTimer(float respawnTime, Tank tank)
        {
            yield return new WaitForSeconds(respawnTime);
            Debug.Log("Timer ends");
            tank.gameObject.SetActive(true);
            tank.Health.Initialize();
        }

    }
}
