using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CannonMovement))]
[RequireComponent(typeof(CannonShoot))]
public class CannonController : MonoBehaviour
{
    private CannonMovement _cannonMovement;
    private CannonShoot _cannonShoot;

    [HideInInspector] public UnityEvent StoneCollisionEvent;

    public int Damage
    {
        set
        {
            _cannonShoot.Damage = value;
        }
    }

    public int FireAmount
    {
        set
        {
            _cannonShoot.FireAmount = value;
        }
    }

    public float FireRateSec
    {
        set
        {
            _cannonShoot.FireRateSec = value;
        }
    }

    private void Awake()
    {
        _cannonMovement = GetComponent<CannonMovement>();
        _cannonShoot = GetComponent<CannonShoot>();
    }

    private void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _cannonMovement.Target = mousePos;

        if (Input.GetMouseButtonDown(0))
            _cannonShoot.Shoot();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var stone = other.GetComponentInParent<Stone>();

        if (stone)
            StoneCollisionEvent.Invoke();
    }
}
