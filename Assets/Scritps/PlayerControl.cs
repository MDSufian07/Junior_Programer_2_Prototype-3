using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Animator playerAnimation;
    private Rigidbody playerRb;
    public float jumpForce = 10;
    public float gravityModifier = 4;
    public bool isOnGround = true;
    public bool gameOver = false; 


    // Start is called before the first frame update
    void Start()
    {
        playerAnimation = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver) // Check if the game is not over before allowing jumps
        {
            if (Input.GetKeyUp(KeyCode.Space) && isOnGround && !gameOver)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
                playerAnimation.SetTrigger("Jump_trig");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            playerAnimation.SetBool("Death_b",true);
            playerAnimation.SetInteger("DeathType_int", 1);

        }
    }
}