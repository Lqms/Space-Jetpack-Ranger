using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Slide Info", fileName = "Slide", order = 1)]
public class Slide : ScriptableObject
{
    [SerializeField] private Sprite _image;
    [SerializeField] private string _hint;

    public Sprite Image => _image;
    public string Hint => _hint;
}
