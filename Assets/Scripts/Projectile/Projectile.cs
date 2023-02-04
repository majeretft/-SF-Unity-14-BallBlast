using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeSpanSeconds;

    private int _damage;

    public int Damage
    {
        get
        {
            return _damage;
        }
        set
        {
            if (value > 0)
                _damage = value;
        }
    }

    private void Start()
    {
        Destroy(gameObject, _lifeSpanSeconds);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }
}
