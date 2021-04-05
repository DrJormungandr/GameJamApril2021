using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControls : MonoBehaviour
{
    public int speed = 4;
    public int jumpForce = 4;
    public Vector2 offset = new Vector2(0, 0);
    public GameObject bulletPrefab;
    public float fireRate = 0.5F;
    public UnityEvent playerDied;

    public AudioSource jumpSound;
    public AudioSource doubleJumpSound;
    public AudioSource shootSound;
    public AudioSource idleSound;
    
    
    private float nextFire = 0;
    private Rigidbody2D rigidbody;
    private SpriteRenderer sprite;
    private int additionalJumps = 2;
    private GameObject killerLegs;
    private bool facingRight = true;
    private bool onGround;
    



    // Start is called before the first frame update
    void Start()
    {
        idleSound.Play();
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        killerLegs = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            facingRight = true;
            sprite.flipX = false;
            
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            facingRight = false;
            sprite.flipX = true;
        }

      //  transform.Translate(new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0));

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            shootSound.PlayOneShot(shootSound.clip);
            nextFire = Time.time + fireRate;
            Vector2 spawnpoint = (Vector2)transform.position + offset;
            Instantiate(bulletPrefab, spawnpoint, facingRight ? bulletPrefab.transform.rotation : new Quaternion(bulletPrefab.transform.rotation.x, bulletPrefab.transform.rotation.y, bulletPrefab.transform.rotation.z*-1, bulletPrefab.transform.rotation.w));
        }

        if (Input.GetButtonDown("Jump") && (additionalJumps > 0 || transform.GetChild(1).GetComponent<GroundDetection>().onGround))
        {
            float k = additionalJumps > 0 ? 1 : 0.5F;
            additionalJumps--;

            if (additionalJumps > 0)
            {
                doubleJumpSound.PlayOneShot(doubleJumpSound.clip);
            } else
            {
                jumpSound.PlayOneShot(jumpSound.clip);
            }

            if (rigidbody.velocity.y <= 0.2F)
            {
                rigidbody.velocity = Vector2.up * jumpForce * k;
            }
            else
            {
                rigidbody.AddForce(Vector2.up * jumpForce * 0.5F, ForceMode2D.Impulse);
            }
        }

        if (transform.GetChild(1).GetComponent<GroundDetection>().onGround)
        {
            additionalJumps = 1;
        }
        else
        {
        }

        if (rigidbody.velocity.y == 0)
        {
            killerLegs.SetActive(false);

        }
        else
        {
            killerLegs.SetActive(true);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("enemy"))
        {
            playerDied.Invoke();
        }
        if (collision.gameObject.tag == "puddle")
        {
            playerDied.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("enemy"))
        {
            death();
        }
        if (collision.gameObject.tag == "puddle")
        {
            death();
        }
        if (collision.gameObject.tag == "boss")
        {
            death();
        }
    }

    private void death()
    {
        rigidbody.velocity = new Vector2(0,0);
        gameObject.active = false;
        playerDied.Invoke();
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0), ForceMode2D.Force);
        rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
    }
}
