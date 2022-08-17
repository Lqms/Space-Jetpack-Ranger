using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WaveDisplay : MonoBehaviour
{
    [SerializeField] private Text _wave;
    [SerializeField] private float _timeToType = 2;
    [SerializeField] private float _timeToShow = 2;
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _fadeColor;
    [SerializeField] private Color _blinkColor;

    private Coroutine _activeCoroutine;

    private void OnEnable()
    {
        DifficultyManager.Instance.LevelUpped += OnLevelUpped;
        DifficultyManager.Instance.BossLevelUpped += OnBossLevelUpped;

        if (LevelManager.CurrentWave % LevelManager.WaveMultiplicityForBoss == 0)
            OnBossLevelUpped();
        else
            OnLevelUpped(PlayerManager.Instance.Wave);
    }

    private void OnDisable()
    {
        DifficultyManager.Instance.LevelUpped -= OnLevelUpped;
        DifficultyManager.Instance.BossLevelUpped -= OnBossLevelUpped;
    }

    private void OnLevelUpped(int level)
    {
        string text = $"WAVE {level}...";

        if (_activeCoroutine != null)
            StopCoroutine(_activeCoroutine);

        _activeCoroutine = StartCoroutine(TypeTextCoroutine(text));
    }

    private IEnumerator TypeTextCoroutine(string text)
    {
        _wave.DOText(text, _timeToType);
        yield return new WaitForSeconds(_timeToShow);
        _wave.text = "";
        _activeCoroutine = null;
    }

    private IEnumerator BlinkTextCoroutine(string text)
    {
        float blinkTime = 0.5f;
        _wave.color = _blinkColor;
        _wave.DOText(text, _timeToType);
        yield return new WaitForSeconds(_timeToType);

        for (int i = 0; i < 3; i++)
        {
            _wave.color = _blinkColor;
            yield return new WaitForSeconds(blinkTime);
            _wave.color = _fadeColor;
            yield return new WaitForSeconds(blinkTime);
        }

        yield return new WaitForSeconds(_timeToShow);
        _wave.text = "";
        _wave.color = _normalColor;
        _activeCoroutine = null;
    }

    private void OnBossLevelUpped()
    {
        string text = $"BOSS!";

        if (_activeCoroutine != null)
            StopCoroutine(_activeCoroutine);

        _activeCoroutine = StartCoroutine(BlinkTextCoroutine(text));
    }
}
