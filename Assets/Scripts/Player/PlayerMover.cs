using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _boostPower;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Thrust(Vector2 direction)
    {
        _rigidbody.AddForce(direction * _boostPower * Time.deltaTime);
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector2.zero;
    }
}
