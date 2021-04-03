using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBullet : MonoBehaviour
{
    public int speed = 3;
    public float timetoLive = 3;
    // Start is called before the first frame update
    UnityEvent playerDied;
    void Start()
    {
        Destroy(gameObject, timetoLive);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "boss" && collision.tag != "trigger")
        {
            Destroy(gameObject);
        }
        if (collision.tag == "player")
        {

            playerDied.Invoke();
        }
    }
}
