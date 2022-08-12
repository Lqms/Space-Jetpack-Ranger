using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Energy : MonoBehaviour
{
    [SerializeField] private float _max = 100;
    [SerializeField] private float _wastePerSecond = 2;

    private float _current;
    private bool _isRunOut = false;

    public float Max => _max;
    public float Current => _current;

    public event UnityAction RunOut;
    public event UnityAction Restored;

    private void OnEnable()
    {
        _current = _max;
    }

    private void FixedUpdate()
    {
        if (_current <= 0 && _isRunOut == false)
        {
            _isRunOut = true;
            RunOut?.Invoke();
            return;
        }

        if (_current > 0)
            _current -= Time.fixedDeltaTime * _wastePerSecond;
    }

    public void Restore(float amount)
    {
        _current = Mathf.Clamp(_current + amount, 0, _max);

        if (_current > 0)
        {
            Restored?.Invoke();
            _isRunOut = false;
        }
    }

    public void SetEnergy(int energy)
    {
        _max = energy;
        _current = _max;
    }
}
