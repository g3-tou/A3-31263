using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        startPos = transform.position; //wherever pacstu is
        targetPos = startPos;
    }

    // Update is called once per frame
    //currently does not move all the way using lastInput etc, just by pressing the key 1 at a time.
    void Update()
    {
        if (!isMoving) //if pacstu is not moving
        {
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
                startPos = transform.position;
                targetPos = startPos + moveDirection * dist; //essentially moves by 1 unit (so like from pellet to pellet.. kinda)
                t = 0f;
                isMoving = true;

                // from PacStuMove
                if (moveDirection.x != 0)
                {
                    anim.SetFloat("Horiz", moveDirection.x);
                    anim.SetFloat("Vert", 0f);
                }
                else if (moveDirection.y != 0)
                {
                    anim.SetFloat("Vert", moveDirection.y);
                    anim.SetFloat("Horiz", 0f);
                }
            }
        }

        if (isMoving) //if pacstu is moving, lerp
        {
            t += Time.deltaTime * speed / dist;
            transform.position = Vector2.Lerp(startPos, targetPos, t);

            if (t >= 1f) 
            {
                transform.position = targetPos;
                isMoving = false;
            }
        }
    }
}

