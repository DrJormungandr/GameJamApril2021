using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossStartScript : MonoBehaviour
{
    public UnityEvent BossBattleStart;
    public GameObject camera;
    public GameObject player;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var audio = player.GetComponent<PlayerControls>().idleSound;
        audio.Stop();
        audio.clip = clip;
        audio.Play();
        BossBattleStart.Invoke();
        camera.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
