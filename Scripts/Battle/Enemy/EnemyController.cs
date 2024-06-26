using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main enemy's class
/// </summary>
namespace RoSS
{
    public class EnemyController : MonoBehaviour
    {
        Spaceship _spaceship;
        [SerializeField] bool _isAttacking = true; //Temporary attack switch

        void Start()
        {
            _spaceship = GetComponent<Spaceship>();
        }


        void Update()
        {
            if (BattleManager.Instance.BattleState == BattleState.Battle && _isAttacking) AttackPlayer();
        }

        void AttackPlayer()
        {
            foreach (Weapon weapon in _spaceship.weaponList)
            {
                if (weapon.CanShoot()) weapon.Shoot();
            }
        }
    }
}