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
    [SerializeField] private int _GeneratePointInCircleTrys = 999;
    private Vector2 _EnemyColliderSize;
    private float _UnitColliderRadius;
    
    public float _SpawnerRadius;

    
    private void Start()
    {
        StartCoroutine(InfiniteSpawn());
    }

    // Спавн юнитов внутри радиуса спавнера с учетом размера коллайдера юнита.
    // Т.е. юнит при спавне будет целиком внутри радиуса спавнера
    private IEnumerator InfiniteSpawn()
    {
        _EnemyColliderSize = _EnemyPool.GetEnemyColliderSize();
        // Радиус коллайдера с доп офсетом для спавна
        _UnitColliderRadius = _SpawnEnemyOffset + (float)Math.Sqrt((Math.Pow(_EnemyColliderSize.x, 2) + (Math.Pow(_EnemyColliderSize.y, 2)))) / 2;
        
        while (!_EnemyPool._PoolReady)
        {
            // Ждем пока сгенерируется пул
            yield return null;
        }
        
        while (true) 
        {
            Vector3 spawnerPosition = transform.position;

            Vector2 randomSpawnPoint = Vector2.zero;
            // Т.к. точки генерируются в квадрате в который вписан круг с радиусом спавнера,
            // то нужно проверять не сгенерировалась ли точка за радиусом
            // Пробуем сгенерировать по разу за кадр пока не заспавним нужную точку
            while (true)
            {
                // Радиус спавна с учетом радиуса юнита (что бы юнит не спавнился частью за радиусом спавнера)
                float finalSpawnerRadius = _SpawnerRadius - _UnitColliderRadius;
                
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

            _EnemyPool.SpawnEnemy(randomSpawnPoint, _EnemysContainer.transform);
            
            //Debug.Log($"spawnerPosition = {spawnerPosition} // randomPoint = {randomSpawnPoint}");
            //Debug.Log(Physics2D.OverlapCircle(randomSpawnPoint, _UnitColliderRadius, _CheckLayersForSpawn));
            yield return new WaitForSeconds(Random.Range(0.01f, 0.011f));
        }
    }

    
    private bool IsPointInsideSpawnerRadius(Vector2 point, float unitRadius)
    {
        Vector2 circleCenter = transform.position;

        if ((Math.Pow(point.x - circleCenter.x, 2) + (Math.Pow(point.y - circleCenter.y, 2)) <= (Math.Pow(_SpawnerRadius - unitRadius, 2))))
        {
            //Debug.Log("IsPointInsideSpawnerRadius true");
            return true;
            
        }
        else
        {
            //Debug.Log("IsPointInsideSpawnerRadius false");
            return false;
        }
    }
}
