using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool walkable = true;
    public bool current = false;
    public bool target = false;
    public bool selectable = false;

    public List<Tile> adjacencyList = new List<Tile>();

    //Needed BFS (breadth first search)
    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;

    //for A*
    public float f = 0;// g + h 
    public float g = 0;// cost from parent to current tile 
    public float h = 0;// cost from the process tile to destination 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (current)
        {
            GetComponent<Renderer>().enabled = true;
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if (target)
        {
            GetComponent<Renderer>().enabled = true;
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (selectable)
        {
            GetComponent<Renderer>().enabled = true;
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void Reset()
    {
      adjacencyList.Clear();

      //walkable = true;
      current = false;
      target = false;
      selectable = false;

      visited = false;
      parent = null;
      distance = 0;

      f = g = h = 0;
    }

    public void FindNeighbors(float jumpHeight, Tile target)
    {
        Reset();

        CheckTile(Vector3.forward, jumpHeight,target);//frente e quanto pula 
        CheckTile(-Vector3.forward, jumpHeight,target);//trás  e quanto pula 
        CheckTile(Vector3.right, jumpHeight,target);//direita
        CheckTile(-Vector3.right, jumpHeight,target);//esquerda
    }

    public void CheckTile(Vector3 direction, float jumpHeight, Tile target)
    { 
        Vector3 halfExtents = new Vector3(0.25f ,(1+jumpHeight)/2.0f ,  0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach(Collider item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if (tile != null && tile.walkable)// player ou objeto
            {
                RaycastHit hit;

                if(!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1)||(tile == target))
                {
                    adjacencyList.Add(tile);
                }
           
            }
        }
    }
}
