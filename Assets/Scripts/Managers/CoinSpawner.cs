using UnityEngine;
using UnityEngine.Events;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private StoneSpawner _stoneSpawner;
    [SerializeField] private Coin _coinPrefab;
    [SerializeField][Range(0, 100)] private int _spawnThreshold;

    [HideInInspector] public UnityEvent<Coin> CoinSpawnedEvent;

    private void Awake()
    {
        _stoneSpawner.StoneSpawnedEvent.AddListener(OnStoneSpawned);
    }

    private void OnDestroy()
    {
        _stoneSpawner.StoneSpawnedEvent.RemoveListener(OnStoneSpawned);
    }

    private void OnStoneSpawned(GameObject stoneObject)
    {
        var ctrl = stoneObject.GetComponent<StoneController>();

        ctrl.StoneDistructedEvent.AddListener(SpawnCoin);
        ctrl.StoneDividedEvent.AddListener(OnStoneDivided);
    }

    private void SpawnCoin(Vector3 position)
    {
        if (Random.Range(0, 100) > _spawnThreshold)
        {
            var coin = Instantiate(_coinPrefab, position, Quaternion.identity);
            CoinSpawnedEvent.Invoke(coin);
        }
    }

    private void OnStoneDivided(StoneController stoneController)
    {
        stoneController.StoneDistructedEvent.AddListener(SpawnCoin);
    }
}
