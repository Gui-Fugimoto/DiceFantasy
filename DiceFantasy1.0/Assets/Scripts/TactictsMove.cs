using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TactictsMove : MonoBehaviour
{
    public bool turn = false;

    List<Tile> selectableTiles = new List<Tile>();
    GameObject[] tiles;

    Stack<Tile> path = new Stack<Tile>();// stack pega o caminho em ordem reversa
    public Tile currentTile;// tile inicial

    public bool moving = false;
    public int move;// quantos tiles irá andar
    public float jumpHeight = 2;
    public float moveSpeed = 2;
    public float jumpVelocity = 4.5f;
    public bool isPlayer;

    Vector3 velocity = new Vector3();// velocidade do mov player
    Vector3 heading = new Vector3();// direcao 

    float halfHeight = 0;// distancia do tile ate o centro do player 

    bool fallingDown = false;
    bool jumpingUp = false;
    bool movingEdge = false;
    Vector3 jumpTarget;

    public Tile actualTargetTile;


    //stats:
    public int MaxHealthStat = 10;
    public int CurrentHealthStat = 1;
    public int AttackStat = 4;
    public int ShieldStat = 0;
    public int RangeStat = 1;
    public int SurplusDamage;
    public int EnemyAttackStat;

    GameObject target;
    [SerializeField]
    NPCMove NPCScript;
    [SerializeField]
    PlayerMove PlayerScript;

    public bool npcDead;

    public bool playerLost;

    [SerializeField]
    LevelSystem level;

    public int Script { get; private set; }

    public bool canSelect;

    [SerializeField]
    ChoicesUI choices;

    
    public void Init()
    {
        CheckIfPlayer();
        tiles = GameObject.FindGameObjectsWithTag("Tile");

        halfHeight = GetComponent<Collider>().bounds.extents.y;

        TurnManager.AddUnit(this);
        CurrentHealthStat = MaxHealthStat;
    }

    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;// identifica o tipo do tile
    }

    public Tile GetTargetTile(GameObject target)
    {

        RaycastHit hit;
        Tile tile = null;

        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
        {
            tile = hit.collider.GetComponent<Tile>();
        }

        return tile;
    }

    public void ComputeAdjacencyLists(float jumpHeight, Tile target)
    {

        tiles = GameObject.FindGameObjectsWithTag("Tile");


        foreach (GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighbors(jumpHeight, target);
        }
    }

    public void FindSelectableTiles()
    {
        ComputeAdjacencyLists(jumpHeight, null);
        GetCurrentTile();

        Queue<Tile> process = new Queue<Tile>();

        process.Enqueue(currentTile);
        currentTile.visited = true;
        //currentTile.parent=? leave as null

        while (process.Count > 0)
        {
            Tile t = process.Dequeue();

            selectableTiles.Add(t);
            t.selectable = true;

            if (t.distance < move) //&& diceValue>0)
            {
                foreach (Tile tile in t.adjacencyList)
                {
                    if (!tile.visited)
                    {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                }
            }
        }
    }

    public void MoveToTile(Tile tile)
    {
        path.Clear();
        tile.target = true;
        moving = true;

        Tile next = tile;
        while (next != null)
        {
            path.Push(next);
            next = next.parent;
        }
    }

    public void Move()
    {
        if (path.Count > 0)
        {
            Tile t = path.Peek();
            Vector3 target = t.transform.position;

            //Calculate units position on top of the target file 
            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target) >= 0.05f)
            {
                bool jump = transform.position.y != target.y;

                if (jump)
                {
                    Jump(target);
                }
                else
                {
                    CalculateHeading(target);
                    SetHorizontalVelocity();
                }

                //Locomotion
                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                //tile center reached
                transform.position = target;
                path.Pop();
            }

        }
        else
        {
            CheckRange();
            RemoveSelectableTiles();
            moving = false;
            TurnManager.EndTurn();

        }
    }

    protected void RemoveSelectableTiles()
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }
        foreach (Tile tile in selectableTiles)
        {
            tile.Reset();
        }

        selectableTiles.Clear();
    }

    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }

    void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }

    void Jump(Vector3 target)
    {
        if (fallingDown)
        {
            FallDownward(target);
        }
        else if (jumpingUp)
        {
            JumpUpward(target);
        }
        else if (movingEdge)
        {
            MoveToEdge();
        }
        else
        {
            PrepareJump(target);
        }
    }

    void PrepareJump(Vector3 target)
    {
        float targetY = target.y;

        target.y = transform.position.y;

        CalculateHeading(target);

        if (transform.position.y > targetY)
        {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = true;

            jumpTarget = transform.position + (target - transform.position) / 2.0f;//distancia target do currentposition
        }
        else
        {
            fallingDown = false;
            jumpingUp = true;
            movingEdge = false;

            velocity = heading * moveSpeed / 3.0f;

            float difference = targetY - transform.position.y;

            velocity.y = jumpVelocity * (0.5f + difference / 2.0f);
        }
    }

    void FallDownward(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y <= target.y)
        {
            fallingDown = false;// esqueci isso e estava pulando bugado, entrava no tile
            jumpingUp = false;
            movingEdge = false;


            Vector3 p = transform.position;
            p.y = target.y;
            transform.position = p;

            velocity = new Vector3();
        }
    }

    void JumpUpward(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y > target.y)// ja pulou sobre a edge da tile?
        {
            jumpingUp = false;
            fallingDown = true;
        }
    }
    void MoveToEdge()
    {
        if (Vector3.Distance(transform.position, jumpTarget) >= 0.05f)
        {
            SetHorizontalVelocity();
        }
        else
        {
            movingEdge = false;
            fallingDown = true;

            velocity /= 5.0f;// diminui a horizontal
            velocity.y = 1.5f;//boost upward
        }
    }

    protected Tile FindLowestF(List<Tile> list)
    {
        Tile lowest = list[0];

        foreach (Tile t in list)
        {
            if (t.f < lowest.f)
            {
                lowest = t;
            }
        }

        list.Remove(lowest);

        return lowest;
    }

    protected Tile FindEndTile(Tile t)
    {
        Stack<Tile> tempPath = new Stack<Tile>();

        Tile next = t.parent;
        while (next != null)
        {
            tempPath.Push(next);
            next = next.parent;
        }

        if (tempPath.Count <= move)
        {
            return t.parent;
        }

        Tile endTile = null;
        for (int i = 0; i <= move; i++)
        {
            endTile = tempPath.Pop();
        }

        return endTile;
    }

    protected void FindPath(Tile target)
    {
        ComputeAdjacencyLists(jumpHeight, target);
        GetCurrentTile();

        List<Tile> openList = new List<Tile>();
        List<Tile> closedList = new List<Tile>();

        openList.Add(currentTile);
        //currentTile.parent=??
        currentTile.h = Vector3.Distance(currentTile.transform.position, target.transform.position);
        currentTile.f = currentTile.h;

        while (openList.Count > 0)
        {
            Tile t = FindLowestF(openList);

            closedList.Add(t);

            if (t == target)
            {
                actualTargetTile = FindEndTile(t);
                MoveToTile(actualTargetTile);
                return;
            }

            foreach (Tile tile in t.adjacencyList)
            {
                if (closedList.Contains(tile))
                {
                    // do nothing, already process 
                }
                else if (openList.Contains(tile))
                {
                    float tempG = t.g + Vector3.Distance(tile.transform.position, t.transform.position);

                    if (tempG < tile.g)
                    {
                        tile.parent = t;

                        tile.g = tempG;
                        tile.f = tile.g + tile.h;
                    }
                }
                else
                {
                    tile.parent = t;

                    tile.g = t.g + Vector3.Distance(tile.transform.position, t.transform.position);
                    tile.h = Vector3.Distance(tile.transform.position, target.transform.position);
                    tile.f = tile.g + tile.h;

                    openList.Add(tile);
                }

            }

        }

        // what do you do if tha is not path to the target tile?
        Debug.Log("Path not found");
    }

    public void BeginTurn()
    {
        Debug.Log("dota2");
        StartCoroutine(DelayBeginTurn());
    }

    public void EndTurn()
    {
        turn = false;
    }

    public void FindNearestTarget()
    {
        if (isPlayer == true)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("NPC");
            float[] Distances = new float[targets.Length];
            GameObject nearest = null;
            float distance = Mathf.Infinity;
            int contador = 0;
            foreach (GameObject objs in targets)
            {
                float d = Vector3.Distance(transform.position, objs.transform.position);
                Distances[contador] = float.MaxValue;
                if (d < distance)
                {
                    Distances[contador] = d;
                    distance = d;
                    nearest = objs;
                }
                contador++;
            }

            target = nearest;
        }
        else if (isPlayer == false)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
            GameObject nearest = null;
            float distance = Mathf.Infinity;

            foreach (GameObject objs in targets)
            {
                float d = Vector3.Distance(transform.position, objs.transform.position);

                if (d < distance)
                {
                    distance = d;
                    nearest = objs;
                }
            }

            target = nearest;
        }


    }

    void CheckRange()
    {
        FindNearestTarget();
        float d = Vector3.Distance(transform.position, target.transform.position);

        if (d <= RangeStat)
        {
            canSelect = true;
            ShowAttackActionHUD();
        }
        else
        {
            canSelect = false;
        }
    }

    void ShowAttackActionHUD()
    {
        //adicionarhud
       // choices.Activate();
        AttackAction();
    }
    public void AttackAction()
    {
        FindNearestTarget();
        if (isPlayer == true)
        {
            GameObject target = GameObject.FindGameObjectWithTag("NPC");
            NPCScript.TakeDamage();
            
        }
        else if (isPlayer == false)
        {
            GameObject target = GameObject.FindGameObjectWithTag("Player");
            PlayerScript.TakeDamage();
            
        }
    }


    void CheckIfPlayer()
    {
        if (gameObject.tag == "Player")
        {
            isPlayer = true;
        }
        else if (gameObject.tag == "NPC")
        {
            isPlayer = false;
        }
    }

    public void CheckDeath()
    {
        Debug.Log("4");
        if (CurrentHealthStat <= 0)
        {
            Debug.Log("mortinho");
            if (!isPlayer)
            {
                npcDead = true;
                level.KilledEnemy();
                playerLost = false;
            }
            else if (isPlayer)
            {
                playerLost = true;
            }
            Destroy(gameObject);
        }
    }
    public void CheckMaxHealth()
    {
        if (CurrentHealthStat > MaxHealthStat)
        {
            CurrentHealthStat = MaxHealthStat;
        }
    }
    public void CheckShieldStat()
    {
        if (EnemyAttackStat >= ShieldStat)
        {
            SurplusDamage = EnemyAttackStat - ShieldStat;
            ShieldStat = 0;
            Debug.Log("2");
        }
        else if (EnemyAttackStat < ShieldStat)
        {
            SurplusDamage = 0;
            ShieldStat -= EnemyAttackStat;
        }
    }

    public void DefineEnemyAttacker()
    {
        FindNearestTarget();
        if (isPlayer == true)
        {
            EnemyAttackStat = target.GetComponent<NPCMove>().AttackStat;
            Debug.Log("1Player");
        }
        if (isPlayer == false)
        {
            EnemyAttackStat = target.GetComponent<PlayerMove>().AttackStat;
            Debug.Log("1NPC");

        }

        
    }

    public IEnumerator DelayBeginTurn()
    {
        
        yield return new WaitForSeconds(3f);
        turn = true;
        Debug.Log("Dota3");
        

    }
    public IEnumerator DelayEndTurn()
    {

        yield return new WaitForSeconds(3f);
        turn = false;


    }

    
}


