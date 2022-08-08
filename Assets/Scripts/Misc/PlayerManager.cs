using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public int WaveNumber { get; private set; } = 1;
    public int MoneyAmount { get; private set; } = 0;

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
        SaveMoney();
    }

    public void TrySaveWave()
    {
        if (LevelManager.CurrentWave % LevelManager.WaveMultiplicityToSave == 0)
        {
            PlayerPrefs.SetInt(Wave, LevelManager.CurrentWave);
            WaveNumber = PlayerPrefs.GetInt(Wave);
        }
    }

    public void IncreaseMoney(int amount)
    {
        MoneyAmount += amount;
    }

    public void SaveMoney()
    {
        PlayerPrefs.SetInt(Money, MoneyAmount);
    }
}
