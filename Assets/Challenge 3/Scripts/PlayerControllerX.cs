using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;
    public float floatForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;
    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;
    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    private float bounceForce = 0.5f;

    private bool isBalloonLowEnough = true; // Add this boolean variable

    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    void Update()
    {
        if (IsGrounded() && !gameOver)
        {
            playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && !gameOver && transform.position.y < 15 && isBalloonLowEnough)
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("BalloonControl"))
        {
            // Set the isBalloonLowEnough boolean based on your criteria
            isBalloonLowEnough = true; /* Your criteria to check if the balloon is low enough */;
        }
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        float rayLength = 0.6f;
        if (Physics.Raycast(ray, rayLength))
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * bounceForce), playerRb.velocity.z);
            return true;
        }
        return false;
    }
}
