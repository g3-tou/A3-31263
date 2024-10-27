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
    private float dist = 1f;
    private Animator anim;
    private bool isMoving = false; //is pacstu moving or not 
    private Vector2 lastInput; //storing last input of pacstu
    private Vector2 currentInput; //current input for pacstu 
    public List<Tilemap> wallTilemaps;
    public Tilemap map;
    public Tile pellets;
    public Tile powerpellets;

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
            PacStuNotMoving();
        }
        //if pacstu is moving, lerp
        if(isMoving){
            PacStuIsMoving();
        }
    }

    void PacStuNotMoving(){
        
        Vector2 moveDirection = Vector2.zero; //no movement at the start of the frame 

        //WASD input to set target position and to move in said position
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection = Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection = Vector2.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection = Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection = Vector2.right;
        }

        if (moveDirection != Vector2.zero) //if the direction is not 0
        {
            lastInput = moveDirection;
            currentInput = lastInput; //makes the current input the last input 
            /* startPos = transform.position;
            targetPos = startPos + lastInput * dist; //essentially moves by 1 unit (so like from pellet to pellet.. kinda)
            t = 0f;
            isMoving = true;*/
            //currentInput = lastInput; //makes the current input the last input 

            // from PacStuMove
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
        if(!isMoving && currentInput != Vector2.zero){
            startPos = transform.position;
            targetPos = startPos + lastInput * dist; //essentially moves by 1 unit (so like from pellet to pellet.. kinda)
            t = 0f;
            isMoving = true;
        }
    }

    void PacStuIsMoving(){
        
        t += Time.deltaTime * speed / dist;
        transform.position = Vector2.Lerp(startPos, targetPos, t);

        if (t >= 1f) 
        {
            transform.position = targetPos;
            isMoving = false;
        }  
    }
}

