﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class Tank : MonoBehaviour
    {

        [SerializeField]
        private string horizontalAxis = "Horizontal";

        [SerializeField]
        private string verticalAxis = "Vertical";

        protected IMover _mover;

        [SerializeField]
        public float moveSpeed, turnSpeed;

        [SerializeField]
        private int _startingHealth;

        private Vector3 _spawnPosition;

        public Weapon Weapon
        {
            get;
            protected set;
        }

        public IMover Mover
        {
            get { return _mover; }
        }

        public Health Health { get; protected set; }

        /// <summary>
        /// After unit respawns place it to the position it was in the start of the game and set it health to starting health.
        /// </summary>

        public void Start()
        {
            _mover = gameObject.GetOrAddComponent<TransformMover>();

            Mover.Init(moveSpeed, turnSpeed);

            Weapon = GetComponentInChildren<Weapon>();
            if (Weapon != null)
            {
                Weapon.Init(gameObject);
            }

            _spawnPosition = transform.position;
        }
        

        protected void Update()
        {
            var input = PlayerInput();
            Mover.Turn(input.x);
            Mover.Move(input.z);
            bool shoot = Input.GetButton("Fire1");
            if (shoot)
            {
                Weapon.Shoot();
            }
        }

        private Vector3 PlayerInput()
        {
            float Movement = Input.GetAxis(verticalAxis);
            float turn = Input.GetAxis(horizontalAxis);
            return new Vector3(turn, 0, Movement);
        }
    }
}