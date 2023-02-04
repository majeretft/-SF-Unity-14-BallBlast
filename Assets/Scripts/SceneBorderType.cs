using UnityEngine;

public enum SceneBorderTypeEnum
{
    Left,
    Right,
    Bottom,
}

public class SceneBorderType : MonoBehaviour
{
    [SerializeField] private SceneBorderTypeEnum _type;

    public SceneBorderTypeEnum BorderType => _type;
}
