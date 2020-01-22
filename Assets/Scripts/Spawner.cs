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
    [SerializeField] private float _SpawnOffset;
    [SerializeField] private LayerMask _CheckLayersForSpawn;
    private BoxCollider2D _EnemyCollider;
    private float _UnitColliderRadius;
    
    
    public float _SpawnerRadius;

    
    private void Start()
    {
        _EnemyCollider = _EnemyPrefab.GetComponent<BoxCollider2D>();
        _UnitColliderRadius = _SpawnOffset + (float)Math.Sqrt((Math.Pow(_EnemyCollider.size.x, 2) + (Math.Pow(_EnemyCollider.size.y, 2)))) / 2;
        
        StartCoroutine(InfiniteSpawn());
    }

    private IEnumerator InfiniteSpawn()
    {
        while (!_EnemyPool._PoolReady)
        {
            yield return null;
        }
        
        while (true) 
        {
            Vector3 spawnerPosition = transform.position;
            Vector2 randomSpawnPoint = new Vector2(spawnerPosition.x - _UnitColliderRadius / 2, spawnerPosition.y - _UnitColliderRadius / 2);

            _EnemyPool.SpawnEnemy(randomSpawnPoint, _EnemysContainer.transform);
            
            Debug.Log($"spawnerPosition = {spawnerPosition} // randomPoint = {randomSpawnPoint}");
            Debug.Log(Physics2D.OverlapCircle(randomSpawnPoint, _UnitColliderRadius, _CheckLayersForSpawn));
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        }
    }
}
