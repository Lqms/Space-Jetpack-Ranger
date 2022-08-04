using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Menu : MonoBehaviour
{
    [SerializeField] private StartAnimator _startAnimator;

    [Header("UI Elements")]
    [SerializeField] private Button _start;
    [SerializeField] private Button _upgrades;
    [SerializeField] private Button _settings;
    [SerializeField] private Button _exit;

    private CanvasGroup _canvasGroup;

    private void OnEnable()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _startAnimator.AnimationFinished += OnAnimationFinished;

        _start.onClick.AddListener(OnStartButtonClicked);
        _upgrades.onClick.AddListener(OnUpgradesButtonClicked);
        _settings.onClick.AddListener(OnSettingsButtonClicked);
        _exit.onClick.AddListener(OnExitButtonClicked);

        _start.interactable = false;
        _upgrades.interactable = false;
        _settings.interactable = false;
        _exit.interactable = false;
    }

    private void OnDisable()
    {
        _startAnimator.AnimationFinished -= OnAnimationFinished;

        _start.onClick.RemoveListener(OnStartButtonClicked);
        _upgrades.onClick.RemoveListener(OnUpgradesButtonClicked);
        _settings.onClick.RemoveListener(OnSettingsButtonClicked);
        _exit.onClick.RemoveListener(OnExitButtonClicked);
    }

    private void OnAnimationFinished()
    {
        _canvasGroup.alpha = 1;

        _start.interactable = true;
        _upgrades.interactable = true;
        _settings.interactable = true;
        _exit.interactable = true;
    }

    private void OnStartButtonClicked()
    {
        Debug.Log("Start");
        SceneManager.LoadScene(1);
    }

    private void OnUpgradesButtonClicked()
    {
        Debug.Log("Upgrades");
    }

    private void OnSettingsButtonClicked()
    {
        Debug.Log("Settings");
    }

    private void OnExitButtonClicked()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
