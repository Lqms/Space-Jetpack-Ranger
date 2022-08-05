using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WaveDisplay : MonoBehaviour
{
    [SerializeField] private Text _wave;
    [SerializeField] private DifficultySetuper _difficultySetuper;

    private Coroutine _activeCoroutine;

    private void OnEnable()
    {
        _difficultySetuper.LevelUpped += OnLevelUpped;

        OnLevelUpped(LevelManager.CurrentLevel);
    }

    private void OnDisable()
    {
        _difficultySetuper.LevelUpped -= OnLevelUpped;
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
