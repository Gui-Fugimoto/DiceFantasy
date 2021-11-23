using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCQuickUI : MonoBehaviour
{
    public GameObject npcStats;

    public Transform targetNPC;

    // Start is called before the first frame update
    void Start()
    {
        npcStats.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("2"))// ativa enquanto aperta npc
        {
            Vector3 playerPos = Camera.main.WorldToScreenPoint(targetNPC.position);
            transform.position = playerPos;

            npcStats.SetActive(true);
        }

        else if (Input.GetKeyUp("2"))// desativa ao parar de apertar npc
        {
            npcStats.SetActive(false);
        }
    }
}
