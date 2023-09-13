using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObstacle : MonoBehaviour
{
    private float initialSpeed = 25; // Default speed
    private float speed;
    private PlayerControl playerControlScript;
    private float leftBound = -15;

    // Start is called before the first frame update
    void Start()
    {
        playerControlScript = GameObject.Find("player").GetComponent<PlayerControl>();
        speed = initialSpeed; // Set the initial speed
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControlScript.gameOver == false)
        {
            // Check if the right arrow key is being held down and speed up accordingly
            if (Input.GetKey(KeyCode.RightArrow))
            {
                speed = initialSpeed + 15; // Increase speed by 10 when right arrow key is held
            }
            else
            {
                speed = initialSpeed; // Reset speed to the initial speed
            }

            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
