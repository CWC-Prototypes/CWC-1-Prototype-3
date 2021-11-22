using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravityModifier = 1;
    public float jumpForce = 10;

    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;

        // A little hop to signal that we're ready to go.
        playerRb.AddForce(Vector3.up * jumpForce / 4);
    }

    // Update is called once per frame
    void Update()
    {
        // Jump when spacebar is pressed.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
    }
}
