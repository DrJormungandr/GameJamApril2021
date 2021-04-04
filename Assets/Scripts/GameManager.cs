using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string levelName = "placeholder";
    public Vector2 lastCheckpoint = new Vector2(0,0);
    public AudioSource playerDeathSound;
    public AudioSource spawnSound;
    public AudioSource enemyDiedSound;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        spawnSound.PlayOneShot(spawnSound.clip);
        lastCheckpoint = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(lastCheckpoint);
    }

    void nextLevel()
    {
        //TODO SceneManager;
    }

    public void playerDied()
    {
        playerDeathSound.PlayOneShot(playerDeathSound.clip);
        player.GetComponent<PlayerControls>().enabled = false;
        StartCoroutine(playerDeath());
    }

    public void enemyDied()
    {
        enemyDiedSound.PlayOneShot(enemyDiedSound.clip);
    }

    IEnumerator playerDeath()
    {
        yield return new WaitForSeconds(2);
        player.transform.position = lastCheckpoint;
        spawnSound.PlayOneShot(spawnSound.clip);
        player.GetComponent<PlayerControls>().enabled = true;
    }

}
