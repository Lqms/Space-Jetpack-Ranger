using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusEnergy : Upgrade
{
    [SerializeField] private int _bonus = 2;
    [SerializeField] private int _decreaser = 5;

    protected override void OnEnable()
    {
        base.OnEnable();

        Cost.text = "COST: " + (PlayerManager.Instance.Energy / _decreaser + Counter).ToString() + "K";
    }

    protected override void OnBuyButtonClicked()
    {
        if (PlayerManager.Instance.TrySpendMoney((PlayerManager.Instance.Energy / _decreaser + Counter) * Multiplier))
        {
            PlayerManager.Instance.IncreaseEnergy(_bonus);
            Cost.text = "COST: " + (PlayerManager.Instance.Energy / _decreaser + Counter).ToString() + "K";
            Counter++;
        }
    }
}

