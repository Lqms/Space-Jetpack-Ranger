using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoDisplay : MonoBehaviour
{
    [SerializeField] private StartAnimator _startAnimator;
    [SerializeField] private Image _image;

    private void OnEnable()
    {
        _startAnimator.Moved += OnRangerMoved;
    }

    private void OnDisable()
    {
        _startAnimator.Moved -= OnRangerMoved;
    }

    private void OnRangerMoved(float progress)
    {
        _image.fillAmount = progress;
    }
}
