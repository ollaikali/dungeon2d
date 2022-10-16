using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Rigidbody2D, Animator are types and we name variables under these types
    Vector2 moveInput;
    [SerializeField] float runSpeed = 10f;
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        //mpvement of player when hitting move control button
        Run();
        //rotating player
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void Run()
    {

        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerVelocity;

        bool playerIsRunning = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;

        playerAnimator.SetBool("isRunning", playerIsRunning);
    }

    void FlipSprite()
    {
        // if number (Mathf.Abs makes any negative or positive number on x coorinate to be positive number) more than 
        // 0.0000001 (which is represented by Mathf.Epsilon) then UPON pressing button on x coordinate (left or right)
        // scale player sprite on z coordinate to negative or positive (represented by Mathf.Sign which determines if it is 
        // + or - when pressing move buttons) which is basically rotating rigid body of the sprite by assigning new Vector2.
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x), 1f);
        }
    }
}
