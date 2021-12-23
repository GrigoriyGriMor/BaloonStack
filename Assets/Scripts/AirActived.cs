using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirActived : MonoBehaviour
{
    private GameObject currentObject;

    public void AirActive(GameObject currentObject, float timeActived)
    {
        this.currentObject = currentObject;

        timeActived = Random.Range(1.0f, timeActived);

        Invoke("AirActiveInvoke", timeActived);
    }

    private void AirActiveInvoke()
    {
        Debug.Log(" spawn ");

        AirScript airScript = currentObject.GetComponent<AirScript>();

        airScript.isActive = true;
        airScript.boxCollider.enabled = true;
        currentObject.SetActive(true);
    }


}
