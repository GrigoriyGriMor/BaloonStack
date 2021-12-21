using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/// <summary>
/// скрипт весит на пузырьке воздуха
/// </summary>
public class AirScript : MonoBehaviour
{
    //[Header("Цвет пузырька")]
    //public Color color;

    //[Header("Id пузырька")]
    //public int id;


    [Header("Тип игрока для шарика")]
    [SerializeField]
    public TypeOfPlayer typeOfPlayer;

    [SerializeField]
    [Header(" Ссыль на партикл")]
    private ParticleSystem particleSys;

    [Header("Кол - во воздуха")]
    public int countAir;

    //[SerializeField]
    //private TMP_Text multipleTextPro;

    [SerializeField]
    private TextMesh multipleText;

    [SerializeField] 
    private float speed = 1; // Скорость сдува

    //[HideInInspector]
    public bool isActive = true;

    private BoxCollider boxCollider;

    [Header("Префаб MultipleTextPro ")]
    [SerializeField]
    private GameObject MultipleTextPro;

    private void Start()
    {
       // GameObject tempMultipleTextPro =  Instantiate(MultipleTextPro,);
       //tempMultipleTextPro.GetComponent<MultipleTextPro>().transformObject = gameObject.transform;

        boxCollider = GetComponent<BoxCollider>();
       // multipleText.text = "X" + countAir.ToString();
    }

    public void Hide()
    {
        isActive = false;
        boxCollider.enabled = false;
        StartCoroutine(SetVisibility(new Vector3(0, 0, 0)));
        if (particleSys)
        {
            particleSys.Play();
        }

    }

    private IEnumerator SetVisibility(Vector3 finish)
    {
        while (true)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, finish, speed * Time.deltaTime);

            if ((finish.magnitude + 0.1) > transform.localScale.magnitude)
            {
                transform.parent.gameObject.SetActive(false);

                break;
            }

           // Debug.Log(" Disable Air ");

            yield return null;
        }
    }
}








