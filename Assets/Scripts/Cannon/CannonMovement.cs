using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private List<SpriteRenderer> _wheels;
    [SerializeField][Range(0, 5)] private float _vehicleWidth;

    private Vector3 _target;
    private float _wheelAngularSpeed;

    public Vector3 Target
    {
        set
        {
            var left = SceneBorder.Instance.LeftBorderX;
            var right = SceneBorder.Instance.RightBorderX;

            if (value.x < left + _vehicleWidth / 2)
                value.x = left + _vehicleWidth / 2;
            if (value.x > right - _vehicleWidth / 2)
                value.x = right - _vehicleWidth / 2;

            _target = value;
        }
    }

    private void Start()
    {
        _target = transform.position;
    }

    private void Update()
    {
        var prevX = transform.position.x;
        Move();
        RotateWheel(prevX);
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(_target.x, transform.position.y, transform.position.z), Time.deltaTime * _speed);
    }

    private void RotateWheel(float prevX)
    {
        var dx = transform.position.x - prevX;

        if (Mathf.Abs(dx) <= 0.01f)
            return;

        _wheelAngularSpeed = _speed * 180 / _wheels[0].bounds.size.x / Mathf.PI;
        foreach (var w in _wheels)
        {
            var vector = dx < 0 ? Vector3.forward : Vector3.back;
            w.transform.Rotate(vector * _wheelAngularSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(
            new Vector3(transform.position.x - _vehicleWidth / 2, transform.position.y - 0.5f, transform.position.z),
            new Vector3(transform.position.x + _vehicleWidth / 2, transform.position.y - 0.5f, transform.position.z)
        );
    }
}
