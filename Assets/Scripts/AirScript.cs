using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ������ ����� �� �������� �������
/// </summary>
public class AirScript : MonoBehaviour
{
    [Header("���� ��������")]
    public Color color;

    [Header("Id ��������")]
    public int id;

    [Header("��� - �� �������")]
    public int countAir;

    [SerializeField] 
    private float speed = 1; // �������� �����

    private BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void Hide()
    {
        boxCollider.enabled = false;
        StartCoroutine(SetVisibility(new Vector3(0, 0, 0)));
    }

    private IEnumerator SetVisibility(Vector3 finish)
    {
        while (true)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, finish, speed * Time.deltaTime);

            if (finish.magnitude <= transform.localScale.magnitude)
            {
                gameObject.SetActive(false);
                break;
            }
            Debug.Log("local = " + transform.localScale.x + "   finish =" + finish.x);

            yield return null;
        }
    }
}








