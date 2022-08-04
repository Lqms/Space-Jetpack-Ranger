using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelHints : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Text _hint;
    [SerializeField] private Button _start;
    [SerializeField] private Button _next;
    [SerializeField] private Button _previous;

    [SerializeField] private Slide[] _slides;

    private int _slideIndex;

    private const string GameSceneName = "Game";

    private void OnEnable()
    {
        _next.onClick.AddListener(OnNextButtonClick);
        _previous.onClick.AddListener(OnPreviousButtonClick);
        _start.onClick.AddListener(OnStartButtonClick);
    }

    private void OnDisable()
    {
        _next.onClick.RemoveListener(OnNextButtonClick);
        _previous.onClick.RemoveListener(OnPreviousButtonClick);
        _start.onClick.RemoveListener(OnStartButtonClick);
    }

    private void OnNextButtonClick()
    {
        _slideIndex++;
        SetupSlide(_slideIndex);
    }

    private void OnPreviousButtonClick()
    {
        _slideIndex--;
        SetupSlide(_slideIndex);
    }

    private void SetupSlide(int index)
    {
        _image.sprite = _slides[index].Image;
        _hint.text = _slides[index].Hint;

        _previous.interactable = index != 0;
        _next.interactable = index != _slides.Length - 1;
    }

    private void OnStartButtonClick()
    {
        SceneManager.LoadScene(GameSceneName);
    }
}
