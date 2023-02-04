using System.Linq;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    [SerializeField] private StoneSpawner _stoneSpawner;

    private int _stoneMax;
    private int _stoneDestroyed;
    private int _stoneDivisions = 2;

    private int _currentLevel;

    public int StoneMax => _stoneMax;
    public int StoneDestroyed => _stoneDestroyed;
    public int CurrentLevel
    {
        get
        {
            return _currentLevel;
        }
        set
        {
            if (value > 0)
                _currentLevel = value;
        }
    }


    private void Update()
    {
        _stoneMax = _stoneSpawner.SpawnAmount * _stoneDivisions;
    }

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

        ctrl.StoneDividedEvent.AddListener(OnStoneDivided);
    }

    private void OnStoneDivided(StoneController stoneController)
    {
        stoneController.StoneDistructedEvent.AddListener(OnStoneDestroyed);
    }

    private void OnStoneDestroyed(Vector3 pos)
    {
        _stoneDestroyed++;
    }
}
