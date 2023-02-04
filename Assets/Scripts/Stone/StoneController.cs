using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Distructible), typeof(Stone))]
public class StoneController : MonoBehaviour
{
    [SerializeField] private Stone _stonePrefab;

    private Distructible _distructible;
    private Stone _stone;
    private StoneMovement _stoneMovement;

    [HideInInspector] public UnityEvent<Vector3> StoneDistructedEvent;
    [HideInInspector] public UnityEvent<StoneController> StoneDividedEvent;

    private void Awake()
    {
        _distructible = GetComponent<Distructible>();
        _stone = GetComponent<Stone>();
        _stoneMovement = GetComponent<StoneMovement>();

        _distructible.DistructedEvent.AddListener(OnDistracted);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var projectile = other.GetComponentInParent<Projectile>();

        if (projectile != null)
        {
            _distructible.ApplyDamage(projectile.Damage);
        }
    }

    private void OnDistracted()
    {
        if (_stone.StoneType != StoneEnum.Small)
            DivideStone();

        StoneDistructedEvent.Invoke(transform.position);
        Destroy(gameObject);
    }

    private void DivideStone()
    {
        for (var i = 0; i < 2; i++)
        {
            var stone = Instantiate(_stonePrefab, transform.position, Quaternion.identity);
            var distructible = stone.GetComponent<Distructible>();
            var movement = stone.GetComponent<StoneMovement>();

            stone.SetType(_stone.StoneType - 1);
            distructible.Hp = _distructible.MaxHp / 2;

            if (i > 0)
                movement.SetHorizontalDirection(_stoneMovement.GetDirection() * -1);
            else
                movement.SetHorizontalDirection(_stoneMovement.GetDirection());

            var controller = stone.GetComponent<StoneController>();
            StoneDividedEvent.Invoke(controller);
        }
    }
}
