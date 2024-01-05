using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;



    public bool extraJump = true;
    public bool isWallRunning = false;

    private void Update()
    {
        //Checks for if the jump key is pressed, the player still has coyote time and the player isn't currently wallrunning
        if (Input.GetKeyDown(playerMovement.jumpKey) && playerMovement.coyoteTimeCounter < 0 && !isWallRunning)
        {
            if (extraJump)
            {
                //If player also has a double jump available, disable the double jump, change the ui to match
                extraJump = false;
                //Zero out the player's y velocity and add jump force to it
                playerMovement.rb.velocity = new Vector3(playerMovement.rb.velocity.x, 0, playerMovement.rb.velocity.z);
                playerMovement.rb.AddForce(transform.up * playerMovement.jumpForce, ForceMode.Impulse);
            }
        }

        if (playerMovement.isGrounded || isWallRunning)
        {
            //If the player is grounded OR the player is wall running, reset the extra jump
            extraJump = true;
        }
    }
}
