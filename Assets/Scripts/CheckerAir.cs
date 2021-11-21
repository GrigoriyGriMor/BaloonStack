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

    private bool isAddAir = true;

    private void OnTriggerEnter(Collider other)
    {
        if (isAddAir)
        {
            if (other.tag == "Air")
            {
                AddAir(other);
            }
        }
    }

    void AddAir(Collider other)
    {
        countAir += other.GetComponent<AirScript>().countAir;
        countAir = Mathf.Clamp(countAir, 0, maxCountAir);
        other.gameObject.SetActive(false);
        if (countAir == maxCountAir)
        {
            isAddAir = false;
        }
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
