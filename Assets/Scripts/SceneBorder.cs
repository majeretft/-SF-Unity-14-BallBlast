using UnityEngine;

public class SceneBorder : MonoBehaviour
{
    private static SceneBorder _instance;

    public static SceneBorder Instance
    {
        get
        {
            return _instance;
        }
    }

    private Vector2 _screenResolution;

    public float LeftBorderX
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector3()).x;
        }
    }

    public float RightBorderX
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(_screenResolution.x, 0, 0)).x;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        DontDestroyOnLoad(gameObject);

        if (Application.isEditor && Application.isPlaying)
        {
            _screenResolution.x = Screen.width;
            _screenResolution.y = Screen.height;
        }
        else
        {
            _screenResolution.x = Camera.main.pixelWidth;
            _screenResolution.y = Camera.main.pixelHeight;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(LeftBorderX, -10, 0), new Vector3(LeftBorderX, 10, 0));
        Gizmos.DrawLine(new Vector3(RightBorderX, -10, 0), new Vector3(RightBorderX, 10, 0));
    }
#endif
}
