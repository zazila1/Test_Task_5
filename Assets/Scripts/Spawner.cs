using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class Spawner : MonoBehaviour
{
    [SerializeField] private PlayerController _Player;
    [SerializeField] private GameObject _EnemyPrefab;
    [SerializeField] private float _SpawnOffset;
    
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
        yield return new WaitForSeconds(Random.Range(0.1f, 3f));
    }
}
