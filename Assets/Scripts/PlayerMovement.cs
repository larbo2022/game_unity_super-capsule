using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
  
    Rigidbody rb;
    [SerializeField] float movementSpeed = 6f;  
    [SerializeField] float jumpForce = 5f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

      [SerializeField] AudioSource jumpSound;
      [SerializeField] AudioSource gameSound;
      [SerializeField] AudioSource destroyEnemySound;

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        gameSound.Play();
    }

    // Update is called once per frame
    void Update()
    {  // Methode classique
       // if (Input.GetKeyDown("space"))
       // {
         //   rb.velocity = new Vector3(0,5f,0);
       //  }

       float horizontalInput = Input.GetAxis("Horizontal");
       float verticalInput = Input.GetAxis("Vertical");

       rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);

       if (Input.GetButtonDown("Jump") && IsGrounded())
       {
           jump();    
       }
       
    }

    void jump()
    {
      rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
      jumpSound.Play();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.CompareTag("Enemy Head"))
      {
        Destroy(collision.transform.parent.gameObject);
        destroyEnemySound.Play();
        jump();
      }
    }

    bool IsGrounded()
    {
      return Physics.CheckSphere(groundCheck.position, ground);
    }  
    
}
