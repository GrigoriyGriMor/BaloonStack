using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// скрипт весит на UIcontroller
/// </summary>
public class UiController : MonoBehaviour
{
    [Header("——ыль на бар игрока кол-во воздуха")]
    [SerializeField]
    private Slider PlayerResult;

    [Header("")]
    [SerializeField]
    private Text currentCountBallonsPlatform;


    

    private void Update()
    {
        UpdateResults();
    }

    void UpdateResults()
    {
        PlayerResult.value = GameController.Instance.countAirPlayer;
        currentCountBallonsPlatform.text = GameController.Instance.platformForBallonsPlayer.currentCountBallonsPlanform.ToString();
    }


}
