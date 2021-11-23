using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Весит на игроке. Соберает воздух
/// </summary>
public class CheckerAir : MonoBehaviour
{
    private MoveController moveController;

    [SerializeField]
    [Header("Мах кол - во воздуха")]
    public int maxCountAir = 10;

    public int countAir;

    [HideInInspector]
    public bool isAddAir;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Air")
        {
            if (countAir == maxCountAir)
            {
            }
            else
            {
                AddAir(other);
            }
        }
        //}
    }

    void AddAir(Collider other)
    {
        AirScript airScript = other.GetComponent<AirScript>();
        countAir += airScript.countAir;
        countAir = Mathf.Clamp(countAir, 0, maxCountAir);
        airScript.Hide();
        isAddAir = true;
        //other.gameObject.SetActive(false);

    }

    public bool DelAir(int countAirDel)
    {
        bool result = false;

        if (countAir > 0)
        {
            result = true;
        }

        countAir -= countAirDel;
        countAir = Mathf.Clamp(countAir, 0, maxCountAir);
        return result;
    }

}
