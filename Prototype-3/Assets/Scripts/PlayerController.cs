using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravityModifier = 1;
    public float jumpForce = 10;

    public ParticleSystem dirtParticle;
    public ParticleSystem explosionParticle;

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
        if (isOnGround && !gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");

            dirtParticle.Stop();

            isOnGround = false;
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;

            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;

            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            dirtParticle.Stop();
            explosionParticle.Play();

            Debug.Log("Game Over");
        }
        else
        {
            // Or anything not tagged "Ground" or "Obstacle."
            Debug.Log("Collision with untagged object: " + collision.gameObject.name);
        }
    }
}
