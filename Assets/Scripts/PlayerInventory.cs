using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private CoinSpawner _coinSpawner;

    private int _coins;

    public int Coins
    {
        get => _coins;
        set {
            if (value > 0)
            _coins = value;
        }
    }

    private void Awake()
    {
        _coinSpawner.CoinSpawnedEvent.AddListener(OnCoinSpawned);
    }

    private void OnDestroy()
    {
        _coinSpawner.CoinSpawnedEvent.RemoveListener(OnCoinSpawned);
    }

    private void OnCoinSpawned(Coin coin)
    {
        coin.CoinCollectedEvent.AddListener(OnCoinCollected);
    }

    private void OnCoinCollected()
    {
        _coins++;
    }

    public bool UseCoins(int amount)
    {
        if (_coins < amount)
            return false;

        _coins -= amount;

        return true;
    }
}
