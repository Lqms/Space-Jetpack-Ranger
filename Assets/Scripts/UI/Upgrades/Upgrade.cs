using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Upgrade : MonoBehaviour
{
    [SerializeField] protected Text Cost;

    protected int Multiplier = 1000;
    protected int Counter = 1;

    private Button _buy;

    protected virtual void OnEnable()
    {
        _buy = GetComponent<Button>();
        _buy.onClick.AddListener(OnBuyButtonClicked);
    }

    private void OnDisable()
    {
        _buy.onClick.RemoveListener(OnBuyButtonClicked);
    }

    protected virtual void OnBuyButtonClicked()
    {
       
    }
}
