using UnityEngine;
using UnityEngine.Events;

public class Distructible : MonoBehaviour
{
    [SerializeField] int _initialHp;

    private int _hp;
    private int _hpMax;
    private bool _isDistructed = false;
    
    public int MaxHp => _hpMax;

    public int Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            if (value < 1)
                return;
            
            _hp = value;
            _initialHp = value;
            _hpMax = value;
        }
    }


    [HideInInspector] public UnityEvent DistructedEvent;

    private void Start()
    {
        if (_hpMax < 1) {
            _hp = _initialHp;
            _hpMax = _initialHp;
        }
    }

    public void ApplyDamage(int damage)
    {
        if (damage < 1 || _isDistructed)
            return;

        _hp -= damage;

        if (_hp <= 0)
        {
            _hp = 0;
            DistructedEvent.Invoke();
            _isDistructed = true;
        }
    }
}
