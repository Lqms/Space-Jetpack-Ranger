using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusHealth : Upgrade
{
    [SerializeField] private int _bonus = 2;

    protected override void OnEnable()
    {
        base.OnEnable();

        Cost.text = "COST: " + (PlayerManager.Instance.Health * Counter).ToString() + "K";
    }

    protected override void OnBuyButtonClicked()
    {
        if (PlayerManager.Instance.TrySpendMoney(PlayerManager.Instance.Health * Counter * Multiplier))
        {
            PlayerManager.Instance.IncreaseHealth(_bonus);
            Cost.text = "COST: " + (PlayerManager.Instance.Health * Counter).ToString() + "K";
            Counter++;
        }
    }
}
