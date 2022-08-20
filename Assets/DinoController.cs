using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoController : MonoBehaviour
{
    public delegate void HitObjectEvent(string objectName);
    public event HitObjectEvent OnHitObject; 

    Vector3 movement = Vector3.right;
    public float Speed = 10f;
    public float JumpForce = 0.4f;
    Rigidbody2D rb;

    bool isGrounded = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += movement * Speed * Time.deltaTime; 
    }

    void FixedUpdate()
    {
        if (isGrounded && Input.GetMouseButton(0))
        {
            rb.AddForce(new Vector2(0,JumpForce), ForceMode2D.Impulse);
        }
    }

    void HitObstacle(string name)
    {
        OnHitObject?.Invoke(name);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        else
        {
            HitObstacle(collision.gameObject.tag);       
        }
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }

        
    }
}
