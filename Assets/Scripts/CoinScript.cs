using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinScript : MonoBehaviour
{
    public UnityEvent addCoin;
    public AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            addCoin.Invoke();
            sound.PlayOneShot(sound.clip);
            Destroy(gameObject);
        }
    }
}
