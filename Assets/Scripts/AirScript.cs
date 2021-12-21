using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ������ ����� �� �������� �������
/// </summary>
public class AirScript : MonoBehaviour
{
    //[Header("���� ��������")]
    //public Color color;

    //[Header("Id ��������")]
    //public int id;

    //[Header("��������� ������ ")]
   // public int 

    [Header("��� ������ ��� ������")]
    [SerializeField]
    public TypeOfPlayer typeOfPlayer;

    [SerializeField]
    [Header(" ����� �� �������")]
    private ParticleSystem particleSys;

    [Header("��� - �� �������")]
    public int countAir;

   

    [SerializeField] 
    private float speed = 1; // �������� �����

    //[HideInInspector]
    public bool isActive = true;

    private BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
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








