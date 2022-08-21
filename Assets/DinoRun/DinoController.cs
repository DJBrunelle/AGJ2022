using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DinoController : MonoBehaviour
{
    public delegate void HitObjectEvent(string objectName);
    public event HitObjectEvent OnHitObject;

    private readonly Vector3 _movement = Vector3.right;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 3f;
    private Rigidbody2D _rb;

    private bool _isGrounded = false;


    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        speed = 4f;
        jumpForce = 3.5f;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += _movement * (speed * Time.deltaTime); 
    }

    private void FixedUpdate()
    {
        if (_isGrounded && Input.GetMouseButton(0))
        {
            _rb.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
        }
    }

    private void HitObstacle(string obstacleName)
    {
        OnHitObject?.Invoke(obstacleName);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
        else
        {
            HitObstacle(collision.gameObject.tag);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}
