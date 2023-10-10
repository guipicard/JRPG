using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SaveData saveData;
    [SerializeField] private Animator animator;

    private bool isRotated = false;
    private bool moving = false;

    public void Save()
    {
        saveData.playerInWorldPosition = transform.position;
    }
    public void Load()
    {
        transform.position = saveData.playerInWorldPosition;
    }

    // Update is called once per frame
    void Update()
    {
        moving = false;
        if (Input.GetKey(KeyCode.D)) 
        {
            Vector2 pos = transform.position;

            pos.x += 1.0f * Time.deltaTime;

            transform.position = pos;

            if(!isRotated)
            {
                transform.Rotate(0, 180, 0);
                isRotated = true;
            }

            animator.SetBool("Walking", true);
            animator.SetBool("WalkingDown", false);
            animator.SetBool("WalkingUp", false);
            moving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector2 pos = transform.position;

            pos.x -= 1.0f * Time.deltaTime;

            transform.position = pos;

            animator.SetBool("Walking", true);
            animator.SetBool("WalkingDown", false);
            animator.SetBool("WalkingUp", false);
            if (isRotated)
            {
                transform.Rotate(0, 180, 0);
                isRotated = false;
            }
            moving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector2 pos = transform.position;

            pos.y -= 1.0f * Time.deltaTime;

            transform.position = pos;

            animator.SetBool("WalkingDown", true);
            animator.SetBool("Walking", false);
            moving = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            Vector2 pos = transform.position;

            pos.y += 1.0f * Time.deltaTime;

            transform.position = pos;

            animator.SetBool("WalkingUp", true);
            animator.SetBool("Walking", false);
            moving = true;
        }
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            animator.SetTrigger("Interact");
        }
        if(!moving)
        {
            animator.SetBool("WalkingUp", false);
            animator.SetBool("WalkingDown", false);
            animator.SetBool("Walking", false);
        }

    }
}
