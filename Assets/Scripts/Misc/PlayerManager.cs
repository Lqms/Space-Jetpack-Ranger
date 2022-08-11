using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private AudioClip _notEnoughtMoney;

    public static PlayerManager Instance { get; private set; }

    public int WaveNumber { get; private set; } = 1;
    public int MoneyAmount { get; private set; } = 0;

    public event UnityAction<float> MoneyChanged;

    private const string Wave = "Wave";
    private const string Money = "Money";


    private void OnEnable()
    {
        if (Instance != null)
            Destroy(this);

        if (Instance == null)
            Instance = this;

        if (Instance == this)
            DontDestroyOnLoad(this);
        else
            Destroy(this);

        if (PlayerPrefs.HasKey(Wave))
            WaveNumber = PlayerPrefs.GetInt(Wave);

        if (PlayerPrefs.HasKey(Money))
            MoneyAmount = PlayerPrefs.GetInt(Money);
    }

    private void OnDisable()
    {
        if (Instance == this)
            Instance = null;
    }

    private void OnApplicationQuit()
    {
        TrySaveWave();
        TrySaveMoney();
    }

    public bool TrySaveWave()
    {
        bool isWaveMultipliced = LevelManager.CurrentWave % LevelManager.WaveMultiplicityToSave == 0;

        if (isWaveMultipliced)
        {
            PlayerPrefs.SetInt(Wave, LevelManager.CurrentWave);
            WaveNumber = PlayerPrefs.GetInt(Wave);
        }

        return isWaveMultipliced;
    }

    public void IncreaseMoney(int amount)
    {
        MoneyAmount += amount;
        MoneyChanged?.Invoke(MoneyAmount);
    }

    public bool TrySaveMoney()
    {
        bool isWaveMultipliced = LevelManager.CurrentWave % LevelManager.WaveMultiplicityToSave == 0;

        if (isWaveMultipliced)
        {
            PlayerPrefs.SetInt(Money, MoneyAmount);
        }

        return isWaveMultipliced;
    }

    public bool TrySpendMoney(int amount)
    {
        if (MoneyAmount < amount)
        {
            AudioManager.Instance.PlayClip(_notEnoughtMoney);
        }
        else
        {
            MoneyAmount -= amount;
            MoneyChanged?.Invoke(MoneyAmount);
        }

        return MoneyAmount > amount;
    }
}
