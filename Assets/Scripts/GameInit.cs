using UnityEngine;

public class GameInit : MonoBehaviour
{
    [SerializeField] private GameObject[] _disableOnInit;
    [SerializeField] private GameObject[] _enableOnInit;

    private void Awake()
    {
        foreach (var go in _disableOnInit)
        {
            go.SetActive(false);
        }

        foreach (var go in _enableOnInit)
        {
            go.SetActive(true);
        }
    }
}
