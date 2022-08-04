using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Button _settings;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Slider _volume;
    [SerializeField] private Button _mainMenu;
    [SerializeField] private Button _exit;

    private void OnEnable()
    {
        _exit.onClick.AddListener(Exit);
        _mainMenu.onClick.AddListener(BackToMainMenu);
        _settings.onClick.AddListener(ToggleSettings);
        _volume.onValueChanged.AddListener(ChangeVolume);
    }

    private void OnDisable()
    {
        _exit.onClick.RemoveListener(Exit);
        _mainMenu.onClick.RemoveListener(BackToMainMenu);
        _settings.onClick.RemoveListener(ToggleSettings);
        _volume.onValueChanged.RemoveListener(ChangeVolume);
    }

    private void ToggleSettings()
    {
        _settingsPanel.SetActive(!_settingsPanel.activeSelf);
        Time.timeScale = _settingsPanel.activeSelf ? 0 : 1;
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void ChangeVolume(float newValue)
    {
        AudioListener.volume = newValue;
    }
}
