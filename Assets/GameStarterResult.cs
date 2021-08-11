using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameStarterResult : MonoBehaviour
{
    InputAction m_goTitle;
    InputAction m_Restart;

    private void Start()
    {
        m_goTitle = GetComponent<PlayerInput>().currentActionMap["GoTitle"];
        m_Restart = GetComponent<PlayerInput>().currentActionMap["Restart"];
    }

    private void Update()
    {
        if (m_goTitle.triggered)
        {
            SceneManager.LoadScene("Title");
        }
        if (m_Restart.triggered)
        {
            SceneManager.LoadScene("Game");
        }
    }
}
