using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private AudioClip _notEnoughtMoney;
    [SerializeField] private AudioClip _succesfullBuy;

    [Header("Player stats")]
    [SerializeField] private int _health = 250;
    [SerializeField] private int _energy = 100;
    [SerializeField] private int _damage = 25;

    public static PlayerManager Instance { get; private set; }

    public int Wave { get; private set; } = 1;
    public int Money { get; private set; } = 0;
    public int Health => _health;
    public int Energy => _energy;
    public int Damage => _damage;

    public event UnityAction<float> MoneyChanged;

    private const string WAVE = "Wave";
    private const string MONEY = "Money";
    private const string DAMAGE = "Damage";
    private const string HEALTH = "Health";
    private const string ENERGY = "Energy";


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

        if (PlayerPrefs.HasKey(WAVE))
            Wave = PlayerPrefs.GetInt(WAVE);

        if (PlayerPrefs.HasKey(MONEY))
            Money = PlayerPrefs.GetInt(MONEY);

        if (PlayerPrefs.HasKey(DAMAGE))
            _damage = PlayerPrefs.GetInt(DAMAGE);

        if (PlayerPrefs.HasKey(HEALTH))
            _health = PlayerPrefs.GetInt(HEALTH);

        if (PlayerPrefs.HasKey(ENERGY))
            _energy = PlayerPrefs.GetInt(ENERGY);
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
        SaveDamage();
        SaveEnergy();
        SaveHealth();
    }

    public bool TrySaveWave()
    {
        bool isWaveMultipliced = LevelManager.CurrentWave % LevelManager.WaveMultiplicityToSave == 0;

        if (isWaveMultipliced)
        {
            PlayerPrefs.SetInt(WAVE, LevelManager.CurrentWave);
            Wave = PlayerPrefs.GetInt(WAVE);
        }

        return isWaveMultipliced;
    }

    public void IncreaseMoney(int amount)
    {
        Money += amount;
        MoneyChanged?.Invoke(Money);
        TrySaveMoney();
    }

    public bool TrySaveMoney()
    {
        bool isWaveMultipliced = LevelManager.CurrentWave % LevelManager.WaveMultiplicityToSave == 0;

        if (isWaveMultipliced)
        {
            PlayerPrefs.SetInt(MONEY, Money);
        }

        return isWaveMultipliced;
    }

    public void SaveDamage()
    {
        PlayerPrefs.SetInt(DAMAGE, _damage);
    }

    public void SaveHealth()
    {
        PlayerPrefs.SetInt(HEALTH, _health);
    }

    public void SaveEnergy()
    {
        PlayerPrefs.SetInt(ENERGY, _energy);
    }


    public bool TrySpendMoney(int amount)
    {
        if (Money < amount)
        {
            AudioManager.Instance.PlayClip(_notEnoughtMoney);
        }
        else
        {
            AudioManager.Instance.PlayClip(_succesfullBuy);
            Money -= amount;
            MoneyChanged?.Invoke(Money);
        }

        return Money > amount;
    }

    public void IncreaseHealth(int amount)
    {
        _health += amount;
        SaveHealth();
    }

    public void IncreaseDamage(int amount)
    {
        _damage += amount;
        SaveDamage();
    }

    public void IncreaseEnergy(int amount)
    {
        _energy += amount;
        SaveEnergy();
    }
}
