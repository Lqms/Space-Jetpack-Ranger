using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class Parallax : MonoBehaviour
{
    [SerializeField] private float _maxXOffset = 1;
    [SerializeField] private float _speed = 0.05f;

    private float _imagePositionX;
    private RawImage _image;

    private void Start()
    {
        _image = GetComponent<RawImage>();
    }

    private void Update()
    {
        _imagePositionX += _speed * Time.deltaTime;

        if (_imagePositionX > _maxXOffset)
            _imagePositionX = 0;

        _image.uvRect = new Rect(_imagePositionX, _image.uvRect.position.y, _image.uvRect.width, _image.uvRect.height);
    }
}
