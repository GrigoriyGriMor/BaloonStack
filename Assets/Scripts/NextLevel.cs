using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



/// <summary>
/// ������ ����� �� ������ NEXT 
/// </summary>
public class NextLevel : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
