using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsToggler : MonoBehaviour
{
    [SerializeField] private Button _toggler;
    [SerializeField] private GameObject _settingsPanel;

    private void OnEnable()
    {
        _toggler.onClick.AddListener(ToggleSettings);
    }

    private void OnDisable()
    {
        _toggler.onClick.RemoveListener(ToggleSettings);
    }

    private void ToggleSettings()
    {
        _settingsPanel.SetActive(!_settingsPanel.activeSelf);
        Time.timeScale = _settingsPanel.activeSelf ? 0 : 1;
    }
}
