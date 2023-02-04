using System;
using UnityEngine;
using UnityEngine.Events;

public class LevelState : MonoBehaviour
{
    [SerializeField] private CannonController _cannonController;
    [SerializeField] private StoneSpawner _stoneSpawner;

    public UnityEvent LevelCompletedEvent;
    public UnityEvent LevelFailedEvent;

    private bool _isSpawnCompleted;
    private float _searchTimeout = 0.5f;
    private float _searchTimer;

    private void Awake()
    {
        _cannonController.StoneCollisionEvent.AddListener(OnStoneCollision);
        _stoneSpawner.SpanwCompletedEvent.AddListener(OnSpawnCompleted);
    }

    private void OnDestroy()
    {
        _cannonController.StoneCollisionEvent.RemoveListener(OnStoneCollision);
        _stoneSpawner.SpanwCompletedEvent.RemoveListener(OnSpawnCompleted);
    }

    private void OnSpawnCompleted()
    {
        _isSpawnCompleted = true;
    }

    private void OnStoneCollision()
    {
        LevelFailedEvent.Invoke();
    }

    private void Update()
    {
        if (_isSpawnCompleted == false)
            return;

        _searchTimer += Time.deltaTime;

        if (_searchTimer < _searchTimeout)
            return;

        if (FindObjectsOfType<Stone>().Length < 1)
            LevelCompletedEvent.Invoke();
    }
}
