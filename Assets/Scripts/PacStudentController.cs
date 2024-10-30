using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PacStudentController : MonoBehaviour
{
    //currently reusing stuff from PacStuMove to implement movement based on WASD keys
    private Vector2 startPos;
    private Vector2 targetPos;
    private float t = 0f;
    private float speed = 5f;
    //private float dist = 1f;
    private Animator anim;
    private bool isMoving = false; //is pacstu moving or not
    private Vector2 lastInput; //storing last input of pacstu
    private Vector2 currentInput; //current input for pacstu
    public List<Tilemap> wallTilemaps;
    //public List<Tilemap> pelletTilemaps;
    //public Tile pellets;
    //public Tile powerpellets;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        startPos = transform.position; //wherever pacstu is
        targetPos = startPos;
        lastInput = Vector2.zero;
        currentInput = Vector2.zero;
    }

    // Update is called once per frame
    //currently does not move all the way using lastInput etc, just by pressing the key 1 at a time.
    void Update()
    {
        //if pacstu is not moving
        if(!isMoving){
            PlayerInput();
            TryToMove();
        }
        //if pacstu is moving, lerp
        if(isMoving){
            PacStuMove();
        }
    }

    void PlayerInput(){
        //WASD input to set target position and to move in said position
        if (Input.GetKey(KeyCode.W)) lastInput = Vector2.up;
        if (Input.GetKey(KeyCode.S)) lastInput = Vector2.down;
        if (Input.GetKey(KeyCode.A)) lastInput = Vector2.left;
        if (Input.GetKey(KeyCode.D)) lastInput = Vector2.right;
    }

    void TryToMove(){
        Vector2 direction = lastInput.normalized;
        Vector2 nextPos = (Vector2)transform.position + direction;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f); //check for walls
        if (hit.collider == null){ 
            if (isWalkable(nextPos)){
                currentInput = lastInput;
                StartMovement(direction);
            }
        }
        else if (currentInput != Vector2.zero){
            nextPos = (Vector2)transform.position + currentInput;
            if (isWalkable(nextPos)){
                StartMovement(currentInput);
            }
        }
    }

    void StartMovement(Vector2 direction){
        startPos = transform.position;
        //targetPos = startPos + lastInput * dist; //essentially moves by 1 unit (so like from pellet to pellet.. kinda)
        targetPos = (Vector2)transform.position + direction;
        t = 0f;
        isMoving = true;

        UpdateAnimationDirection();
    }

    void UpdateAnimationDirection(){
        if (currentInput.x != 0)
        {
            anim.SetFloat("Horiz", currentInput.x);
            anim.SetFloat("Vert", 0f);
        }
        else if (currentInput.y != 0)
        {
            anim.SetFloat("Vert", currentInput.y);
            anim.SetFloat("Horiz", 0f);
        }
    }

    void PacStuMove(){
        t += Time.deltaTime * speed / Vector2.Distance(startPos, targetPos);
        transform.position = Vector2.Lerp(startPos, targetPos, t);

        if (t >= 1f)
        {
            transform.position = targetPos;
            isMoving = false;
        }
    }

    bool isWalkable(Vector2 position){
        //RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);
        //return hit.collider == null || !hit.collider.CompareTag("Wall");
        foreach (var tilemap in wallTilemaps){
            Vector3Int gridPos = tilemap.WorldToCell(position);
            if (tilemap.GetTile(gridPos) != null){
                return false;
            }
        }
        return true;
    }
}

