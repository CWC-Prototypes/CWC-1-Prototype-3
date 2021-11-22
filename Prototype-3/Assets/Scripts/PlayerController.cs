using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravityModifier = 1;
    public float jumpForce = 10;

    public bool isOnGround = true;
    public bool gameOver = false;
    private Rigidbody playerRb;
    private Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();

        Physics.gravity *= gravityModifier;

        // A little hop to signal that we're ready to go.
        playerRb.AddForce(Vector3.up * jumpForce / 4);
    }

    // Update is called once per frame
    void Update()
    {
        // Jump when spacebar is pressed.
        if (isOnGround && Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            isOnGround = false;
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over");
        }
        else
        {
            Debug.Log("Collision with untagged object: " + gameObject.name);
        }
    }
}
