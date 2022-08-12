using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private Text _moneyText;

    private int _divider = 1000;

    private void OnEnable()
    {
        PlayerManager.Instance.MoneyChanged += OnMoneyChanged;
        _moneyText.text = $"{PlayerManager.Instance.Money / _divider}K";
    }

    private void OnDisable()
    {
        PlayerManager.Instance.MoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(float moneyValue)
    {
        string newText = $"{System.Math.Round(moneyValue / _divider, 1)}K";
        _moneyText.text = newText;
    }
}
