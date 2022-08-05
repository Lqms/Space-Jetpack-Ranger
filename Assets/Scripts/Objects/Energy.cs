using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Energy : MonoBehaviour
{
    [SerializeField] private float _max = 100;
    [SerializeField] private float _wastePerSecond = 2;

    private Rigidbody2D _rigidBody;
    private float _current;

    public float Max => _max;
    public float Current => _current;

    private const float GravityScale = 0.05f;

    private void OnEnable()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.gravityScale = 0;
        _current = _max;
    }

    private void FixedUpdate()
    {
        if (_current <= 0)
        {
            _rigidBody.gravityScale = GravityScale;
            return;
        }

        _current -= Time.fixedDeltaTime * _wastePerSecond;
    }

    public void Restore(float amount)
    {
        _current = Mathf.Clamp(_current + amount, 0, _max);

        if (_current > 0)
            _rigidBody.gravityScale = 0;
    }
}
