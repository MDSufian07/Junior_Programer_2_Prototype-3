using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Animator playerAnimation;
    private Rigidbody playerRb;
    private AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    public ParticleSystem drifParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce = 10;
    public float gravityModifier = 4;
    public bool isOnGround = true;
    public bool gameOver = false;

    private int jumpsRemaining = 2; // Maximum jumps allowed, including the initial jump.

    // Start is called before the first frame update
    void Start()
    {
        playerAnimation = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver) // Check if the game is not over before allowing jumps
        {
            if (Input.GetKeyDown(KeyCode.Space) && (isOnGround || jumpsRemaining > 0))
            {
                playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z); // Reset vertical velocity
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
                playerAnimation.SetTrigger("Jump_trig");
                drifParticle.Stop();
                playerAudio.PlayOneShot(jumpSound, 2.0f);
                jumpsRemaining--;

                if (jumpsRemaining < 0)
                {
                    jumpsRemaining = 0; // Ensure jumpsRemaining doesn't go negative
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            drifParticle.Play();
            jumpsRemaining = 2; // Reset jumps when landing on the ground
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            playerAnimation.SetBool("Death_b", true);
            playerAnimation.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound, 2.0f);
            drifParticle.Stop();
        }
    }
}
