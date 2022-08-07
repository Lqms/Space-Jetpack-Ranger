using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public int WaveNumber { get; private set; } = 1;

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
    }

    private void OnDisable()
    {
        if (Instance == this)
            Instance = null;
    }

    private void OnApplicationQuit()
    {
        TrySaveWave();
    }

    public void TrySaveWave()
    {
        if (LevelManager.CurrentWave % LevelManager.WaveMultiplicityToSave == 0)
        {
            PlayerPrefs.SetInt(Wave, LevelManager.CurrentWave);
            WaveNumber = PlayerPrefs.GetInt(Wave);
        }
    }
}
