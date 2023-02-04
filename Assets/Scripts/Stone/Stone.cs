using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum StoneEnum
{
    Small,
    Medium,
    Large,
    Big,
}

[RequireComponent(typeof(Distructible))]
public class Stone : MonoBehaviour
{
    [SerializeField] private StoneEnum _stoneType;
    [SerializeField] private TextMeshProUGUI _text;

    private Distructible _distructible;

    private static Dictionary<StoneEnum, Vector3> _sizes = new Dictionary<StoneEnum, Vector3>
    {
        { StoneEnum.Big, new Vector3(1, 1, 1) },
        { StoneEnum.Large, new Vector3(0.85f, 0.85f, 0.85f) },
        { StoneEnum.Medium, new Vector3(0.75f, 0.75f, 0.75f) },
        { StoneEnum.Small, new Vector3(0.5f, 0.5f, 0.5f) },
    };

    public StoneEnum StoneType => _stoneType;

    private void Awake()
    {
        _distructible = GetComponent<Distructible>();
    }

    private void Start()
    {
        if (_distructible.Hp > 999)
            _text.text = $"{_distructible.Hp / 1000}k";
        else
            _text.text = _distructible.Hp.ToString();
    }

    private void Update()
    {
        _text.text = _distructible.Hp.ToString(); ;
    }

    public void SetType(StoneEnum type)
    {
        if (type < 0)
            return;

        _stoneType = type;

        transform.localScale = Stone._sizes[type];
    }

    public void SetMaxHp(int hp)
    {
        _distructible.Hp = hp;
    }
}
