using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Coin : MonoBehaviour
{
    [SerializeField] private float _fallAcceleration;

    [HideInInspector] public UnityEvent CoinCollectedEvent;

    private Animator _animator;
    private float _speed;

    public Vector3 Position
    {
        set
        {
            transform.position = value;
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _speed -= _fallAcceleration * Time.deltaTime;

        transform.Translate(0, _speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var borderType = other.GetComponent<SceneBorderType>();
        var cannon = other.GetComponentInParent<CannonController>();

        if (borderType != null)
            if (borderType.BorderType == SceneBorderTypeEnum.Bottom)
                EnableIdleAnimation();

        if (cannon != null)
            CollectCoin();
    }

    public void EnableIdleAnimation()
    {
        _animator.enabled = true;
        enabled = false;
    }

    public void CollectCoin()
    {
        CoinCollectedEvent.Invoke();
        Destroy(gameObject);
    }
}
