using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suicider : Enemy
{
    [SerializeField] private AudioClip _beep;

    private SpriteRenderer _spriteRenderer;
    private Color _startColor = Color.white;
    private Color _blinkColor = new Color(1, 0, 0, 0.8f);

    protected override void OnEnable()
    {
        base.OnEnable();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = _startColor;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            Speed = 0;
            StartCoroutine(BlinkCoroutine());
        }
    }

    private IEnumerator BlinkCoroutine()
    {
        for (int i = 0; i < 3; i++)
        {
            _spriteRenderer.color = _blinkColor;
            AudioManager.Instance.PlayClip(_beep);
            yield return new WaitForSeconds(0.2f);
            _spriteRenderer.color = _startColor;
            yield return new WaitForSeconds(0.2f);
        }

        DestroyedByPlayer = false;
        Health.ApplyDamage(Health.Max);
    }

    protected override void OnDied()
    {
        base.OnDied();

        SpawnerContainer.ExplosionSpawner.Spawn(transform.position);
    }
}
