using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class Spawner : MonoBehaviour
{
    [SerializeField] private PlayerController _Player;
    [SerializeField] private GameObject _EnemyPrefab;
    [SerializeField] private GameObject _EnemysContainer;
    [SerializeField] private EnemyPoolController _EnemyPool;
    [SerializeField] private float _SpawnEnemyOffset;
    [SerializeField] private LayerMask _CheckLayersForSpawn;

    [SerializeField] private int _RewardForEnemy;
    
    private Vector2 _EnemyColliderSize;
    private float _UnitColliderRadius;
    
    public float _SpawnerRadius;

    
    private void Start()
    {
        _EnemyColliderSize = _EnemyPool.GetEnemyColliderSize();
        StartCoroutine(InfiniteSpawn());
    }

    // Спавн юнитов внутри радиуса спавнера с учетом размера коллайдера юнита.
    // Т.е. юнит при спавне будет целиком внутри радиуса спавнера
    private IEnumerator InfiniteSpawn()
    {
        Collider2D[] overlapResults = new Collider2D[5];
        
        // Радиус коллайдера с доп офсетом для спавна
        _UnitColliderRadius = _SpawnEnemyOffset + (float)Math.Sqrt((Math.Pow(_EnemyColliderSize.x, 2) + (Math.Pow(_EnemyColliderSize.y, 2)))) / 2;
        
        while (!_EnemyPool._PoolReady)
        {
            // Ждем пока сгенерируется пул
            yield return null;
        }
        
        // Основной цикл спавнера
        while (true) 
        {
            Vector3 spawnerPosition = transform.position;
            Vector2 randomSpawnPoint;
            
            // Т.к. точки генерируются в квадрате в который вписан круг с радиусом спавнера,
            // то нужно проверять не сгенерировалась ли точка за радиусом
            // Пробуем сгенерировать по разу за кадр пока не заспавним нужную точку
            while (true)
            {
                float randomX = Random.Range(spawnerPosition.x - _SpawnerRadius, spawnerPosition.x + _SpawnerRadius);
                float randomY = Random.Range(spawnerPosition.y - _SpawnerRadius, spawnerPosition.y + _SpawnerRadius);
                randomSpawnPoint = new Vector2(randomX, randomY);

                if (IsPointInsideSpawnerRadius(randomSpawnPoint, _UnitColliderRadius))
                {
                    // Если точка попала в круг, то выходим из генерации
                    break;
                }
                
                yield return null;
            }
            
            // Если в месте спавна есть объекты блокирующие спавн, то запускаем цикл опять
            if (Physics2D.OverlapCircleNonAlloc(randomSpawnPoint, _UnitColliderRadius, overlapResults, _CheckLayersForSpawn) > 0)
            {
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                // Достаем из пула врага и инициализируем его данные с нуля
                Enemy enemy = _EnemyPool.SpawnEnemy(randomSpawnPoint, _EnemysContainer.transform);
                
                enemy.Setup((reward =>
                {
                    _Player.OnEnemyKilled(reward);
                    _EnemyPool.RemoveEnemy(enemy); 
                }));
                    
                yield return new WaitForSeconds(Random.Range(0.01f, 0.011f));
            }
            
        }
    }

    
    private bool IsPointInsideSpawnerRadius(Vector2 point, float unitRadius)
    {
        Vector2 circleCenter = transform.position;
        // Проверяется радиус спавна с учетом радиуса юнита (что бы юнит не спавнился частью за радиусом спавнера)
        return Math.Pow(point.x - circleCenter.x, 2) + (Math.Pow(point.y - circleCenter.y, 2)) <= (Math.Pow(_SpawnerRadius - unitRadius, 2));
    }
}
