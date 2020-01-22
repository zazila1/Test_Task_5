using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private int _Health;
    [SerializeField] private int _MaxHealth;
    [SerializeField] private int _Reward;
    private Action<int> _OnEnemyDie;
    
    // Словарь для кеширования
    private static Dictionary<string, Weapon> particlesAttacker = new Dictionary<string, Weapon>();

    private void Update()
    {
    }

    private void Die()
    {
        _OnEnemyDie?.Invoke(_Reward);
    }

    void Damage(int damage)
    {
        _Health -= damage;

        if (_Health <= 0)
        {
            Die();
        }
    }

    public void Setup(Action<int> dieAction)
    {
        // настройка врага после спавна из пула
        _Health = _MaxHealth;
        _OnEnemyDie = dieAction;
    }

    //ParticlePhysicsExtensions.GetCollisionEvents();
    void OnParticleCollision(GameObject attackerWeaponGameObject)
    {
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int collisionCount = attackerWeaponGameObject.GetComponent<ParticleSystem>().GetCollisionEvents(gameObject, collisionEvents);
        
        Debug.Log($"collisionEvents {collisionCount}");
        Debug.Log($"list count = {collisionEvents.Count}");
        
        if (attackerWeaponGameObject.CompareTag("PlayerWeapon"))
        {
            // Кешируем ссылку атакующего или используем кешированную ее для последующих попаданий 
            if (particlesAttacker.ContainsKey(attackerWeaponGameObject.name))
            {
                // Если попадание таким партиклом уже было, то забираем актуальный урон
                Damage(particlesAttacker[attackerWeaponGameObject.name].GetWeaponDamage() * collisionCount);
            }
            else
            {
                particlesAttacker.Add(attackerWeaponGameObject.name, attackerWeaponGameObject.GetComponent<Weapon>());
                Debug.Log("GetComponent");
            }
        }
    }
}
