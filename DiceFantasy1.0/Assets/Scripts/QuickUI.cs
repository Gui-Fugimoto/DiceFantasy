using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickUI : MonoBehaviour
{
    public GameObject playerStats;

    public Transform targetPlayer;

    // Start is called before the first frame update
    void Start()
    {
        playerStats.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("1"))// ativa enquanto aperta player
        {
            Vector3 playerPos = Camera.main.WorldToScreenPoint(targetPlayer.position);
            transform.position = playerPos;

            playerStats.SetActive(true);
        }

        else if (Input.GetKeyUp("1"))// desativa ao parar de apertar player
        {
            playerStats.SetActive(false);
        }
    }
}
