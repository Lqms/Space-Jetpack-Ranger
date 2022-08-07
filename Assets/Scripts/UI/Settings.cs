using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider _volume;
    [SerializeField] private Button _instructions;
    [SerializeField] private Button _statistics;
    [SerializeField] private Button _mainMenu;

    public event UnityAction Closed;

    private void OnEnable()
    {
        _volume.value = AudioListener.volume;
        _volume.onValueChanged.AddListener(ChangeVolume);
        _instructions.onClick.AddListener(OpenInstructions);
        _statistics.onClick.AddListener(OpenStatistics);
        _mainMenu.onClick.AddListener(BackToMainMenu);
    }

    private void OnDisable()
    {
        _volume.onValueChanged.RemoveListener(ChangeVolume);
        _instructions.onClick.RemoveListener(OpenInstructions);
        _statistics.onClick.RemoveListener(OpenStatistics);
        _mainMenu.onClick.RemoveListener(BackToMainMenu);
    }

    private void ChangeVolume(float newValue)
    {
        AudioListener.volume = newValue;
    }

    private void OpenInstructions()
    {
        Debug.Log("Instuctions");
    }

    private void OpenStatistics()
    {
        Debug.Log("Statistics");
    }

    private void BackToMainMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Closed?.Invoke();
            return;
        }

        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
