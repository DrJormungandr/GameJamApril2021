using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DumbAi : MonoBehaviour
{
    // Start is called before the first frame update
    public int walkTime = 5;
    public int speed = 1;
    public GameObject player;
    public bool playerSpotted = false;
    public bool returntoDefault = false;
    private Rigidbody2D rigidBody;
    private Vector2 defaultPosition;
    void Start()
    {
        defaultPosition = transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerSpotted)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);
        }

        if (returntoDefault && !playerSpotted)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(defaultPosition.x, transform.position.y), speed * Time.deltaTime);
        }

    }

    IEnumerator chasePlayerTime()
    {
        yield return new WaitForSeconds(walkTime);
        playerSpotted = false;
        yield return new WaitForSeconds(1);
        returntoDefault = true;
        StartCoroutine(returnToDefaultPos());

    }

    IEnumerator returnToDefaultPos()
    {
        yield return new WaitUntil(() => (Vector2)transform.position == defaultPosition);
        returntoDefault = false;
    }

    public void invokePlayerSpotted()
    {
        if(playerSpotted == false)
        {
            returntoDefault = false;
            StopCoroutine(returnToDefaultPos());
            playerSpotted = true;
            StartCoroutine(chasePlayerTime());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Debug.Log("YOU DIED");
        }
    }

}
