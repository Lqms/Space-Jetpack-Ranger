using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoDisplay : MonoBehaviour
{
    [SerializeField] private RangerMover _ranger;
    [SerializeField] private Image _image;

    private void OnEnable()
    {
        _ranger.Moved += OnRangerMoved;
    }

    private void OnDisable()
    {
        _ranger.Moved -= OnRangerMoved;
    }

    private void OnRangerMoved(float progress)
    {
        _image.fillAmount = progress;
    }
}
