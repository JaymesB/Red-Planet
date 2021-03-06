﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float jumpLength;
    [SerializeField] private float jumpCooldown;

    private SpriteRenderer playerSprite;

    private bool jumping;
    private float timeInAir;
    private float timeSinceLand;

    //Start is called right after Awake, and is for interaction with other objects
    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // FixedUpdate is called once per fixed length of time 
    void FixedUpdate()
    {
        //get raw input 
        float horzInput = Input.GetAxis("Horizontal");
        float vertInput = Mathf.Max(Input.GetAxis("Vertical"), Input.GetAxis("Jump"));

        //face the player sprite the movement direction
        flipSprite(horzInput); 

        //calculate move direction
        Vector2 movement = new Vector2(horzInput * speed, jumpModifier(vertInput));

        //actually move the player
        transform.Translate(movement);

    }


    private float jumpModifier(float vertInput)
    {
        float yMove = 0;

        if (jumping)
        {
            timeSinceLand = 0;

            //apply jump energy
            yMove = jumpSpeed;

            //check if still jumping
            jumping = timeInAir < jumpLength;

            //increment
            timeInAir += Time.deltaTime;
        }
        else
        {
            timeInAir = 0;

            //check if jumping
            jumping = vertInput > 0 && timeSinceLand > jumpCooldown;

            //increment
            timeSinceLand += Time.deltaTime;
        }

        return yMove;
    }

    private void flipSprite(float horzInput)
    {
        if (horzInput > 0)
        {
            playerSprite.flipX = false;
        }
        else if (horzInput < 0)
        {
            playerSprite.flipX = true; 
        }
    }
}
