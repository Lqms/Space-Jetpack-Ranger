using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BountyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _floatingTime = 1;

    private RectTransform _rectTransform;
    private int _bounty;
    private float _baseFloatingTime;

    private void OnEnable()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Setup(int bounty)
    {
        _rectTransform.localScale = new Vector3(1, 1, 1);
        _baseFloatingTime = _floatingTime;
        _bounty = bounty;
        _text.text = $"+{_bounty} ®";
        StartCoroutine(FloatCoroutine());
    }

    private IEnumerator FloatCoroutine()
    {
        float scaleX = 1;
        float scaleY = 1;
        float positionY = _rectTransform.localPosition.y;

        while (_baseFloatingTime > 0)
        {
            _baseFloatingTime -= Time.deltaTime;
            scaleX += Time.deltaTime;
            scaleY += Time.deltaTime;
            positionY += Time.deltaTime;
            _rectTransform.localScale = new Vector3(scaleX, scaleY, 1);
            _rectTransform.localPosition = new Vector3(_rectTransform.localPosition.x, positionY, _rectTransform.localPosition.z);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
