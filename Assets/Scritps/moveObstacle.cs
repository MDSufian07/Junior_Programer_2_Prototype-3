using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObstacle : MonoBehaviour
{
    private float speed=25;
    private PlayerControl playerControlScript;
    private float leftBound = -15;
   
    // Start is called before the first frame update
    void Start()
    {

        playerControlScript = GameObject.Find("player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControlScript.gameOver==false) 
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if(transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
      
    }
}
