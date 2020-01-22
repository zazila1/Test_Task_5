using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolController : MonoBehaviour
{
    [SerializeField] private GameObject _EnemyPrefab;
    
    [SerializeField] [Range(0, 200)] private int _InitPoolSize = 15;

    private Queue<Enemy> _Pool = new Queue<Enemy>();
    private List<Enemy> _SpawnedEnemys = new List<Enemy>();

    public bool _PoolReady = false;

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
            
            //card.WipeCardData();
            //card.ShowCard(false);
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
        
        Debug.Log(enemy.transform.parent.name);
        enemyTransform.SetParent(parentTransform);
        enemyTransform.position = spawnPosition;
        //enemyTransform.localScale = Vector3.one;
        
        _SpawnedEnemys.Add(enemy);
        Debug.Log(enemy.transform.parent.name);
        return enemy;
    }

    // Возвращаем объект в пул, вайпаем дату и прячем
    public void RemoveEnemy(Enemy enemy)
    {
        Transform itemTransform = enemy.transform;
        
        itemTransform.SetParent(transform);
        itemTransform.position = transform.position;
        
        enemy.gameObject.layer = LayerMask.NameToLayer("Hided");
        // categoryItem.WipeItemData();
        // categoryItem.ShowCard(false);
        
        _Pool.Enqueue(enemy);
    }
   
}
