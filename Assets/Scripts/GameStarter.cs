using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _speed;

    public event UnityAction GameStarted;

    private void Update()
    {
        _player.transform.position = Vector2.MoveTowards(_player.transform.position, transform.position, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            GameStarted?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
