using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))] 
public class CloseButton : MonoBehaviour
{
    [SerializeField] private GameObject _closableObject;

    private Button _closeButton;

    private void OnEnable()
    {
        _closeButton = GetComponent<Button>();
        _closeButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(Close);
    }

    private void Close()
    {
        _closableObject.SetActive(false);
    }
}
