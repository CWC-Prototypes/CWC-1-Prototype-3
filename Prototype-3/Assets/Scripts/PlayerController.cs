using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravityModifier = 1;
    public float jumpForce = 10;

    public ParticleSystem dirtParticle;
    public ParticleSystem explosionParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    public bool isOnGround = true;
    public bool gameOver = false;
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

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

            playerAudio.PlayOneShot(jumpSound, 1.0f);

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
            playerAudio.PlayOneShot(crashSound, 1.0f);
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
