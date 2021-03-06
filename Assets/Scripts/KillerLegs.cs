using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KillerLegs : MonoBehaviour
{
    public int killJumpForce = 5;
    public UnityEvent enemyDied;
    private Rigidbody2D parentRb;
    // Start is called before the first frame update
    void Start()
    {
       parentRb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            parentRb.AddForce(Vector2.up * killJumpForce, ForceMode2D.Impulse);
            enemyDied.Invoke();
            Destroy(collision.gameObject);
        }
    }
}
