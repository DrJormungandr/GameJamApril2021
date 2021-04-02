using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public int speed = 4;
    public int jumpForce = 4;
    private int gravity = 3;
    private Rigidbody2D rigidbody;
    private int jumps = 2;
    private bool jumpInitiated = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0));
        if (Input.GetButtonDown("Jump") && jumps > 0)
        {
            float k = jumps > 1 ? 1 : 0.5F;
            jumps--;
            if (rigidbody.velocity.y <= 0.2F) { 
                rigidbody.velocity = Vector2.up * jumpForce * k;
            } else
            {
                rigidbody.AddForce(Vector2.up*jumpForce * 0.5F, ForceMode2D.Impulse);
            }
        }
        Debug.Log(rigidbody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumps = 2;
    }
}
