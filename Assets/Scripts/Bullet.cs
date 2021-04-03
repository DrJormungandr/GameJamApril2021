using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed = 3;
    public float timetoLive = 3;
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
        if (collision.tag != "player" && collision.tag != "trigger")
        {
            Destroy(gameObject);
        }
        if (collision.tag.Contains("enemy"))
        {
            Destroy(collision.gameObject);
        }
    }

}
