using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// скрипт весит на UIcontroller
/// </summary>
public class UiController : MonoBehaviour
{
    [SerializeField]
    private Slider PlayerResult;

    private void Update()
    {
        UpdateResults();
    }

    void UpdateResults()
    {
        PlayerResult.value = GameController.Instance.countAirPlayer;
    }
}
