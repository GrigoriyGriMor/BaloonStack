using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    //[SerializeField]
    private Text textCountFps;
    private int countFps;

    private void Start()
    {
        textCountFps = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        countFps = (int)(1.0f / Time.deltaTime);
        textCountFps.text = countFps.ToString();
    }
}
