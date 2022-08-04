using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    [SerializeField] private Sprite _image;
    [SerializeField] private string _hint;

    public Sprite Image => _image;
    public string Hint => _hint;
}
