using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowEnergyDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private Color _transparent;
    [SerializeField] private Color _low;
    [SerializeField] private Color _deactivated;
    [SerializeField] private float _blinkTime = 0.3f;

    private WaitForSeconds _waitTime;
    private Image _image;
    private Coroutine _blinkCoroutine;

    private void Start()
    {
        _image = GetComponent<Image>();
        _image.color = _transparent;
        _waitTime = new WaitForSeconds(_blinkTime);

        _player.JetpackDeactivated += OnPlayerJetpackDeactivated;
        _player.Energy.RunOut += OnPlayerEnergyRunOut;
        _player.Energy.Restored += OnPlayerEnergyRestored;
    }

    private void OnDisable()
    {
        _player.JetpackDeactivated -= OnPlayerJetpackDeactivated;
        _player.Energy.RunOut -= OnPlayerEnergyRunOut;
        _player.Energy.Restored -= OnPlayerEnergyRestored;
    }

    private void OnPlayerJetpackDeactivated()
    {
        _image.color = _deactivated;

        if (_blinkCoroutine != null)
        {
            StopCoroutine(_blinkCoroutine);
            _blinkCoroutine = null;
        }
    }

    private void OnPlayerEnergyRunOut()
    {
        if (_blinkCoroutine == null)
            _blinkCoroutine = StartCoroutine(BlinkCoroutine());
    }

    private void OnPlayerEnergyRestored()
    {
        _image.color = _transparent;

        if (_blinkCoroutine != null)
        {
            StopCoroutine(_blinkCoroutine);
            _blinkCoroutine = null;
        }
    }

    private IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            _image.color = _low;
            yield return _waitTime;
            _image.color = _transparent;
            yield return _waitTime;
        }
    }
}
