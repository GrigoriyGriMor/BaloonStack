using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// скрипт весит на пузырьке воздуха
/// </summary>
public class AirScript : MonoBehaviour
{
    [Header("Цвет пузырька")]
    public Color color;

    [Header("Id пузырька")]
    public int id;

    [Header("Кол - во воздуха")]
    public int countAir;

    [SerializeField] 
    private float speed = 1; // Скорость сдува

    private BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void Hide()
    {
        boxCollider.enabled = false;
        StartCoroutine(SetVisibility(Vector3.zero));
    }

    private IEnumerator SetVisibility(Vector3 finish)
    {
       
        //var color = spriteRenderer.color;
        while (true)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, finish, speed * Time.deltaTime);

            if (transform.localScale == finish)
            {
                gameObject.SetActive(false);
                break;
            }

            yield return null;
        }



    }
}








