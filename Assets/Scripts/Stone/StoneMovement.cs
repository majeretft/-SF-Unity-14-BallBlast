using UnityEngine;

public class StoneMovement : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _fallSpeed;
    [SerializeField] private float _reboundSpeedY;
    [SerializeField] private float _rotationSpeed;

    private bool _isFall = false;
    private Vector3 _velocity = new Vector3();

    private void Start()
    {
        if (transform.position.x < SceneBorder.Instance.LeftBorderX)
            _velocity.x = _horizontalSpeed;
        else if (transform.position.x > SceneBorder.Instance.RightBorderX)
            _velocity.x = -_horizontalSpeed;
    }

    private void Update()
    {
        TryEnableFall();
        Move();
    }

    private void TryEnableFall()
    {
        if (
            transform.position.x > SceneBorder.Instance.LeftBorderX
            && transform.position.x < SceneBorder.Instance.RightBorderX
        )
            _isFall = true;
    }

    private void Move()
    {
        if (_isFall)
            _velocity.y -= _fallSpeed * Time.deltaTime;

        var velocitySign = Mathf.Sign(_velocity.x);

        _velocity.x = velocitySign * _horizontalSpeed;
        transform.position += _velocity * Time.deltaTime;

        transform.Rotate(0, 0, _rotationSpeed * velocitySign * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var borderType = other.GetComponent<SceneBorderType>();

        if (borderType == null)
            return;

        if (!_isFall)
            return;

        if (borderType.BorderType == SceneBorderTypeEnum.Left
            || borderType.BorderType == SceneBorderTypeEnum.Right)
            _velocity.x *= -1;

        if (borderType.BorderType == SceneBorderTypeEnum.Bottom)
            _velocity.y = _reboundSpeedY;
    }

    public void AddVerticalVelocity(float velocity)
    {
        if (velocity < 0)
            return;

        _velocity.y += velocity;
    }

    public void SetHorizontalDirection(float direction)
    {
        _velocity.x = Mathf.Sign(direction) * _horizontalSpeed;
    }

    public float GetDirection()
    {
        return Mathf.Sign(_velocity.x);
    }
}
