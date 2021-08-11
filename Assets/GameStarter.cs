using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    InputAction m_input;

    private void Start()
    {
        m_input = GetComponent<PlayerInput>().currentActionMap["Play"];
    }

    private void Update()
    {
        if (m_input.triggered)
        {
            SceneManager.LoadScene("Game");
        }
    }
}
