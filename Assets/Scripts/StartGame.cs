using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ������ ����� �� ������ StartGame
/// </summary>
public class StartGame : MonoBehaviour
{
    public void OnStartGame()
    {
        Debug.Log(" Start ");
        gameObject.SetActive(false);
        GameController.Instance.StartGame();
    }
}

