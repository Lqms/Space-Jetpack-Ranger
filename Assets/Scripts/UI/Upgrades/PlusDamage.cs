using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusDamage : Upgrade
{
    [SerializeField] private int _bonus = 2;

    protected override void OnEnable()
    {
        base.OnEnable();

        Cost.text = "COST: " + (PlayerManager.Instance.Damage + Counter ).ToString() + "K";
    }

    protected override void OnBuyButtonClicked()
    {
        if (PlayerManager.Instance.TrySpendMoney((PlayerManager.Instance.Damage + Counter) * Multiplier))
        {
            PlayerManager.Instance.IncreaseDamage(_bonus);
            Cost.text = "COST: " + (PlayerManager.Instance.Damage + Counter).ToString() + "K";
            Counter++;
        }
    }
}
