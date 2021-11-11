using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnText : MonoBehaviour
{
    public float timeMin = 0.05f;
    public float timeMax = 1.2f;

    private float timer;
    private Text flickerTxt;
    // Start is called before the first frame update
    void Start()
    {
        flickerTxt = GetComponent<Text>();
        timer = Random.Range(timeMin, timeMax);
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.deltaTime;

        if (timer <= 0)
        {
            flickerTxt.enabled = !flickerTxt.enabled;
            Random.Range(timeMin, timeMax);
        }
    }
}
