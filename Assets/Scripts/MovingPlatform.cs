using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector2 moveToPos = new Vector2(0, 0);
    public float speed = 2;
    private Vector2 defaultPos;
    private bool moveForward = true;
    // Start is called before the first frame update
    void Start()
    {
        defaultPos = transform.position;
        StartCoroutine(moveUntilDestination());
    }

    // Update is called once per frame
    void Update()
    {
        if (moveForward)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveToPos, speed * Time.deltaTime);
        } else
        {
            transform.position = Vector2.MoveTowards(transform.position, defaultPos, speed * Time.deltaTime);
        }

    }

    IEnumerator moveUntilDestination()
    {
        yield return new WaitUntil(() => (Vector2)transform.position == moveToPos);
        moveForward = false;
        StartCoroutine(moveUntilSource());
    }

    IEnumerator moveUntilSource()
    {
        yield return new WaitUntil(() => (Vector2)transform.position == defaultPos);
        moveForward = true;
        StartCoroutine(moveUntilDestination());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            collision.gameObject.transform.SetParent(transform);
        }       
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }

}
