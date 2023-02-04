using System.Collections.Generic;
using UnityEngine;

public class CannonShoot : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private float _fireRateSec;
    [SerializeField] private int _fireAmount;
    [SerializeField] private float _projectileDx;
    [SerializeField] private float _projectileWidth;
    [SerializeField] private int _damage;

    private float _fireTimer;
    private List<Vector3> _startPositions;

    public float FireRateSec
    {
        get => _fireRateSec;
        set
        {
            if (value > 0) _fireRateSec = value;
        }
    }
    public int FireAmount
    {
        get => _fireAmount;
        set
        {
            if (value > 0) _fireAmount = value;
        }
    }
    public int Damage
    {
        get => _damage;
        set
        {
            if (value > 0) _damage = value;
        }
    }

    private void Start()
    {
        _fireTimer = _fireRateSec;
    }

    private void Update()
    {
        _fireTimer += Time.deltaTime;
        CalculateStartPositions();
    }

    public void Shoot()
    {
        if (_fireTimer >= _fireRateSec)
        {
            foreach (var p in _startPositions)
            {
                var projectile = Instantiate<Projectile>(_projectilePrefab, p, Quaternion.identity);
                projectile.Damage = _damage;
            }

            _fireTimer = 0;
        }
    }

    private void CalculateStartPositions()
    {
        if (_startPositions == null || _fireAmount != _startPositions.Count)
            _startPositions = new List<Vector3>(_fireAmount);

        var allWidth = _fireAmount * _projectileWidth + (_fireAmount - 1) * _projectileDx;
        var halfWidth = allWidth / 2;

        for (var i = 0; i < _fireAmount; i++)
        {
            var temp = _projectileSpawnPoint.position.x - halfWidth + _projectileWidth / 2 + (_projectileDx + _projectileWidth) * i;
            var vec = new Vector3(temp, _projectileSpawnPoint.position.y, _projectileSpawnPoint.position.z);

            _startPositions.Add(vec);
        }
    }

    private void OnDrawGizmosSelected()
    {
        var from = new Vector3(_projectileSpawnPoint.position.x - _projectileDx / 2, _projectileSpawnPoint.position.y - 0.1f, _projectileSpawnPoint.position.z);
        var to = new Vector3(_projectileSpawnPoint.position.x + _projectileDx / 2, _projectileSpawnPoint.position.y - 0.1f, _projectileSpawnPoint.position.z);

        Gizmos.DrawLine(from, to);

        var from1 = new Vector3(_projectileSpawnPoint.position.x - _projectileWidth / 2, _projectileSpawnPoint.position.y - 0.2f, _projectileSpawnPoint.position.z);
        var to1 = new Vector3(_projectileSpawnPoint.position.x + _projectileWidth / 2, _projectileSpawnPoint.position.y - 0.2f, _projectileSpawnPoint.position.z);

        Gizmos.DrawLine(from1, to1);
    }
}
