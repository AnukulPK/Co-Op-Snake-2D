using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public Button buttonPlay;
    public Button buttonMultiPlay;

    private void Awake()
    {
        buttonPlay.onClick.AddListener(PlayGame);
        buttonMultiPlay.onClick.AddListener(PlayGameMultiPlayer);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    private void PlayGameMultiPlayer()
    {
        SceneManager.LoadScene(2);
    }
}
