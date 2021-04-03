using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public int speed = 4;
    public int jumpForce = 4;
    public Vector2 offset = new Vector2(0, 0);
    public GameObject bulletPrefab;
    public float fireRate = 0.5F;
    private float nextFire = 0;
    private Rigidbody2D rigidbody;
    private int additionalJumps = 2;
    private GameObject killerLegs;
    private bool facingRight = true;
    private bool onGround;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        killerLegs = gameObject.transform.GetChild(0).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            facingRight = true;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            facingRight = false;
        }

      //  transform.Translate(new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0));

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Vector2 spawnpoint = (Vector2)transform.position + offset;
            Instantiate(bulletPrefab, spawnpoint, facingRight ? bulletPrefab.transform.rotation : new Quaternion(bulletPrefab.transform.rotation.x, bulletPrefab.transform.rotation.y, bulletPrefab.transform.rotation.z*-1, bulletPrefab.transform.rotation.w));
        }

        if (Input.GetButtonDown("Jump") && (additionalJumps > 0 || transform.GetChild(1).GetComponent<GroundDetection>().onGround))
        {
            float k = additionalJumps > 0 ? 1 : 0.5F;
            additionalJumps--;
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

    private void FixedUpdate()
    {
        rigidbody.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0), ForceMode2D.Force);
        rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
    }
}
