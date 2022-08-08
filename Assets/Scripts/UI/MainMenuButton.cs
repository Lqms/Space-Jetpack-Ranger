using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MainMenuButton : MonoBehaviour
{
    private Button _exitToMainMenu;

    private void OnEnable()
    {
        _exitToMainMenu = GetComponent<Button>();
        _exitToMainMenu.onClick.AddListener(OnExitToMainMenuButtonClicked);
    }

    private void OnDisable()
    {
        _exitToMainMenu.onClick.RemoveListener(OnExitToMainMenuButtonClicked);   
    }

    private void OnExitToMainMenuButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
}
