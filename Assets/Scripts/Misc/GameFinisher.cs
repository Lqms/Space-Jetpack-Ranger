using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinisher : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _speed;

    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _player.Health.Died += EndGame;       
    }

    private void OnDisable()
    {
        _player.Health.Died -= EndGame;
    }

    private void EndGame()
    {
        StartCoroutine(EndGameCoroutine());
    }

    private IEnumerator EndGameCoroutine()
    {
        float scaleY = 0;

        while (scaleY < 1)
        {
            scaleY += Time.deltaTime * _speed;
            _rectTransform.localScale = new Vector3(1, scaleY, 1);
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
