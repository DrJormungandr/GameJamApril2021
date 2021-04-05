using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    public int speed = 3;
    public float timetoLive = 3;
    public UnityEvent enemyDied;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timetoLive);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "player" && collision.gameObject.tag != "trigger")
        {
            Destroy(gameObject);
        }

        if (collision.tag.Contains("enemy"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "player" && collision.gameObject.tag != "trigger" && collision.gameObject.tag != "coin")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag.Contains("enemy"))
        {
            enemyDied.Invoke();
            Destroy(collision.gameObject);
        }
    }

}
