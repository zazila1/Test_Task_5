using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolController : MonoBehaviour
{
    [SerializeField] private GameObject _EnemyPrefab;
    
    [SerializeField] [Range(0, 200)] private int _InitPoolSize = 15;

    private Queue<Enemy> _Pool = new Queue<Enemy>();
    private List<Enemy> _SpawnedEnemys = new List<Enemy>();

    //private BoxCollider2D _EnemyCollider;
    private Vector2 _EnemyColliderSize;
    public bool _PoolReady = false;

    private void Awake()
    {
        _EnemyColliderSize  = _EnemyPrefab.GetComponent<BoxCollider2D>().size;
    }

    void Start()
    {
        FillPool(_InitPoolSize);
        _PoolReady = true;
    }
    
    // Создаем пул врагов
    private void FillPool(int count)
    {
        for (int i = 0; i < _InitPoolSize; i++)
        {
            var enemyGameObject = Instantiate(_EnemyPrefab, transform.position, Quaternion.identity, transform);
            Enemy enemy = enemyGameObject.GetComponent<Enemy>();

            enemyGameObject.layer = LayerMask.NameToLayer("Hided");
            
            _Pool.Enqueue(enemy);
        }
    }
  

    // Достаем врага из пула или создаем нового, если пул пустой
    public Enemy SpawnEnemy(Vector3 spawnPosition, Transform parentTransform)
    {
        // Если пул пустой
        if (_Pool.Count == 0)
        {
            FillPool(_InitPoolSize);
        }
        
        var enemy = _Pool.Dequeue();
        Transform enemyTransform = enemy.transform;
        
        enemyTransform.SetParent(parentTransform);
        enemyTransform.position = spawnPosition;
        
        _SpawnedEnemys.Add(enemy);
        
        enemy.gameObject.layer = LayerMask.NameToLayer("Enemy");
        return enemy;
    }

    // Возвращаем объект в пул, вайпаем дату и прячем
    public void RemoveEnemy(Enemy enemy)
    {
        Transform itemTransform = enemy.transform;
        
        itemTransform.SetParent(transform);
        itemTransform.position = transform.position;
        
        enemy.gameObject.layer = LayerMask.NameToLayer("Hided");
        
        _Pool.Enqueue(enemy);
    }

    public Vector2 GetEnemyColliderSize()
    {
        return _EnemyColliderSize;
    }
   
}
