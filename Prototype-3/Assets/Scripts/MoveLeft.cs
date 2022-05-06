using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 10;

    private bool cleared = false;   // Obstacle has been cleared (and scored).
    private float leftBound = -15;
    private GameObject player;
    private PlayerController playerController;
    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        scoreManager = GameObject.Find("Game Manager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.gameOver == false)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (gameObject.CompareTag("Obstacle"))
        {
            if (transform.position.x < player.transform.position.x && !cleared)
            {
                // Player has cleared obstacle.
                // XXX - may need to calculate based on the size of the Player and obstacle colliders.
                scoreManager.score++;
                cleared = true;
            }
            if (transform.position.x < leftBound)
            {
                // Obstacle is out of sight.
                Destroy(gameObject);
            }
        }
    }
}
