using UnityEngine;

public class ColorScheme : MonoBehaviour
{
    [SerializeField] private Color[] _colors;
    [SerializeField] private StoneSpawner _stoneSpawner;

    private void Awake()
    {
        _stoneSpawner.StoneSpawnedEvent.AddListener(ApplyStoneColor);
    }

    private void OnDestroy() {
        _stoneSpawner.StoneSpawnedEvent.RemoveListener(ApplyStoneColor);
    }

    public void ApplyStoneColor(GameObject stoneObject)
    {
        var renderer = stoneObject.GetComponentInChildren<SpriteRenderer>();

        if (renderer) {
            renderer.color = _colors[Random.Range(0, _colors.Length)];
        }
    }
}
