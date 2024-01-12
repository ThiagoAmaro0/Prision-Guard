
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private RagdollManager _enemyPrefab;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Transform[] _spawnPoints;
    private float _spawnTime;
    private ObjectPool<RagdollManager> _objectPool;

    private void Start()
    {
        _objectPool = new ObjectPool<RagdollManager>(_enemyPrefab.gameObject);
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            Spawn();
        }
    }

    private void Update()
    {
        if (_spawnTime < Time.time && _objectPool.GetCount() < _spawnPoints.Length)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        _spawnTime = Time.time + _spawnDelay;

        RagdollManager enemy = _objectPool.GetObject(_enemyPrefab);
        enemy.OnDisableAction += ReturnToPool;
        enemy.OnPunchAction += DisableObject;
        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        if (spawnPoint.childCount != 0)
        {
            foreach (Transform point in _spawnPoints)
            {
                if (point.childCount == 0)
                {
                    spawnPoint = point;
                    break;
                }
            }
        }
        enemy.transform.parent = spawnPoint;
        enemy.transform.localPosition = Vector3.zero;
    }

    private void ReturnToPool(RagdollManager enemy)
    {
        _objectPool.ReturnToPool(enemy.gameObject);
        enemy.OnDisableAction -= ReturnToPool;
    }

    private void DisableObject(RagdollManager enemy)
    {
        enemy.transform.parent = null;
        _objectPool.DisableObject(enemy.gameObject);
        enemy.OnPunchAction -= DisableObject;
    }
}