using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _Reward;
    [SerializeField] private int _Health;
    public UnityAction<int> _OnEnemyDie;
    // Словарь для кеширования
    private static Dictionary<string, Weapon> particlesAttacker = new Dictionary<string, Weapon>();

    private void Update()
    {
    }

    private void Die()
    {
        _OnEnemyDie?.Invoke(_Reward);
        
        Destroy(gameObject);
    }

    void Damage(int damage)
    {
        _Health -= damage;

        Debug.Log(_Health);
        if (_Health <= 0)
        {
            Die();
        }
    }

    private void Setup(PlayerController player)
    {
        _OnEnemyDie += player.OnEnemyKilled;
    }

    void OnParticleCollision(GameObject attackerWeaponGameObject)
    {
        
        if (attackerWeaponGameObject.CompareTag("PlayerWeapon"))
        {
            // Кешируем ссылку атакующего или используем кешированную ее для последующих попаданий 
            if (particlesAttacker.ContainsKey(attackerWeaponGameObject.name))
            {
                // Если попадание таким партиклом уже было, то забираем актуальный урон
                Damage(particlesAttacker[attackerWeaponGameObject.name].GetWeaponDamage());
            }
            else
            {
                particlesAttacker.Add(attackerWeaponGameObject.name, attackerWeaponGameObject.GetComponent<Weapon>());
                Debug.Log("GetComponent");
            }
        }
    }
}
