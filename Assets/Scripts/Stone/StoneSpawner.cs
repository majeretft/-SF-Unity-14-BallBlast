using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class StoneSpawner : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private Stone _prefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnInterval;

    [Header("Balance")]
    [SerializeField] private CannonShoot _cannonShoot;
    [SerializeField] private int _spawnAmount;
    [SerializeField][Range(0f, 1f)] private float _minHpPercent;
    [SerializeField] private float _maxHpRate;

    [HideInInspector] public UnityEvent<GameObject> StoneSpawnedEvent;
    [HideInInspector] public UnityEvent SpanwCompletedEvent;

    private float _timer;
    private int _spawned;

    private int _stoneMaxHp;
    private int _stoneMinHp;

    public int SpawnAmount
    {
        get
        {
            return _spawnAmount;
        }
        set
        {
            if (value > 0)
                _spawnAmount = value;
        }
    }

    private void Start()
    {
        var cannonDps = Convert.ToInt32(_cannonShoot.Damage * _cannonShoot.FireAmount / _cannonShoot.FireRateSec);

        _stoneMaxHp = Convert.ToInt32(cannonDps * _maxHpRate);
        _stoneMinHp = Convert.ToInt32(_stoneMaxHp * _minHpPercent);

        _timer = _spawnInterval;
    }

    private void Update()
    {
        if (_spawned >= _spawnAmount)
        {
            enabled = false;
            SpanwCompletedEvent.Invoke();
        }


        if (_timer >= _spawnInterval)
        {
            Spawn();
            _timer = 0;
        }

        _timer += Time.deltaTime;
    }

    private void Spawn()
    {
        var stone = Instantiate(_prefab, _spawnPoints[Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity);
        stone.SetType(StoneEnum.Big);
        stone.SetMaxHp(Random.Range(_stoneMinHp, _stoneMaxHp + 1));

        StoneSpawnedEvent.Invoke(stone.gameObject);

        _spawned++;
    }
}
