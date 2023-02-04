using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private CannonController _cannonController;
    [SerializeField] private StoneSpawner _stoneSpawner;
    [SerializeField] private LevelProgress _levelProgress;
    [SerializeField] private PersistantData _persistentData;
    [SerializeField] private PlayerInventory _playerInventory;

    private int _defaultDamage = 1;
    private int _defaultFireAmount = 1;
    private int _defaultLevel = 1;
    private int _defaultCoins = 0;
    private float _defaultFireRate = 0.5f;

    private int _currentDamage;
    private int _currentFireAmount;
    private int _currentLevel;
    private int _currentCoins;
    private float _currentFireRate;

    public void LoadData()
    {
        var data = _persistentData.Read();

        _levelProgress.CurrentLevel =
            _currentLevel =
                data.Level.HasValue && data.Level.Value >= _defaultLevel ? data.Level.Value : _defaultLevel;

        _cannonController.Damage =
            _currentDamage =
                data.Damage.HasValue && data.Damage.Value >= _defaultDamage ? data.Damage.Value : _defaultDamage;

        _cannonController.FireAmount =
            _currentFireAmount =
                data.FireAmount.HasValue && data.FireAmount.Value >= _defaultFireAmount ? data.FireAmount.Value : _defaultFireAmount;

        _cannonController.FireRateSec =
            _currentFireRate =
                data.FireRate.HasValue && data.FireRate.Value >= _defaultFireRate ? data.FireRate.Value : _defaultFireRate;

        _playerInventory.Coins =
            _currentCoins =
                data.Coins.HasValue && data.Coins.Value >= _defaultCoins ? data.Coins.Value : _defaultCoins;

        _stoneSpawner.SpawnAmount = _levelProgress.CurrentLevel + 1;
    }

    private GameData MakeGameData()
    {
        return new GameData
        {
            Coins = _currentCoins,
            Damage = _currentDamage,
            FireAmount = _currentFireAmount,
            FireRate = _currentFireRate,
            Level = _currentLevel,
        };
    }

    public void StartNextLevel()
    {
        _currentLevel++;

        _persistentData.Save(MakeGameData());

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IncreaseDamage()
    {
        if (_playerInventory.UseCoins(1))
        {
            _currentDamage += 1;
            _cannonController.Damage = _currentDamage;
            _persistentData.Save(MakeGameData());
        }
    }

    public void IncreaseFireAmount()
    {
        if (_currentFireAmount < 10 && _playerInventory.UseCoins(1))
        {
            _currentFireAmount += 1;
            _cannonController.FireAmount = _currentFireAmount;
            _persistentData.Save(MakeGameData());
        }
    }

    public void IncreaseFireRate()
    {
        if (_currentFireRate >= 0.2f && _playerInventory.UseCoins(1))
        {
            _currentFireRate -= 0.1f;
            _cannonController.FireRateSec = _currentFireRate;
            _persistentData.Save(MakeGameData());
        }

    }

    public void ResetData()
    {
        _currentDamage = _defaultDamage;
        _currentFireAmount = _defaultFireAmount;
        _currentLevel = _defaultLevel;
        _currentCoins = _defaultCoins;
        _currentFireRate = _defaultFireRate;

        _persistentData.Save(MakeGameData());
        RestartCurrentLevel();
    }

    public void Quit()
    {
        _persistentData.Save(MakeGameData());
        Application.Quit();
    }
}
