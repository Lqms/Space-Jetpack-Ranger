using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WaveDisplay : MonoBehaviour
{
    [SerializeField] private Text _wave;

    private Coroutine _activeCoroutine;

    private void OnEnable()
    {
        DifficultyManager.Instance.LevelUpped += OnLevelUpped;

        OnLevelUpped(PlayerManager.Instance.WaveNumber);
    }

    private void OnDisable()
    {
        DifficultyManager.Instance.LevelUpped -= OnLevelUpped;
    }

    private void OnLevelUpped(int level)
    {
        string text = $"WAVE {level}...";

        if (_activeCoroutine != null)
            StopCoroutine(_activeCoroutine);

        _activeCoroutine = StartCoroutine(TypeTextCoroutine(text, 2));
    }

    private IEnumerator TypeTextCoroutine(string text, float time)
    {
        _wave.DOText(text, time);
        yield return new WaitForSeconds(time);
        _wave.text = "";
        _activeCoroutine = null;
    }
}
