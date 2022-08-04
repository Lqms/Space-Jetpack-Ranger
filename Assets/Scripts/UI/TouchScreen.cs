using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TouchScreen : MonoBehaviour
{
    [SerializeField] private Button _attack;
    [SerializeField] private GameStarter _gameStarter;  

    public event UnityAction AttackButtonPressed;

    private void OnEnable()
    {
        _attack.onClick.AddListener(OnAttackButtonClicked);
        _gameStarter.GameStarted += OnGameStart;
    }

    private void OnDisable()
    {
        _attack.onClick.RemoveListener(OnAttackButtonClicked);
        _gameStarter.GameStarted -= OnGameStart;
    }

    private void OnAttackButtonClicked()
    {
        if (Time.timeScale != 0)
            AttackButtonPressed?.Invoke();
    }

    private void OnGameStart()
    {
        _attack.interactable = true;
    }
}
