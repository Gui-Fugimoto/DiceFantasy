using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuScript 
{
   [MenuItem("Tools/Assign Tile Material")]
    public static void AssignTileMaterial()// adiciona o material em todos os tiles de uma vez 
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        Material material = Resources.Load<Material>("Tile");

        foreach(GameObject t in tiles)
        {
            t.GetComponent<Renderer>().material = material; 
        }
    }

    [MenuItem("Tools/Assign Tile Script")]
    public static void AssigTileScript()// todos os tiles com o script tile 
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject t in tiles)
        {
            t.AddComponent<Tile>();
        }
    }
}
