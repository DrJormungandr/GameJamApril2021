using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string levelName = "placeholder";
    public Vector2 lastCheckpoint = new Vector2(0,0);
    public float victoryDelay = 2;
    public AudioSource playerDeathSound;
    public AudioSource spawnSound;
    public AudioSource enemyDiedSound;
    public AudioSource victorySound;
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

    public void Victory()
    {
        StartCoroutine(victoryWithDelay());
    }

    IEnumerator victoryWithDelay()
    {
        player.GetComponent<PlayerControls>().idleSound.Stop();
        yield return new WaitForSeconds(victoryDelay);
        victorySound.PlayOneShot(victorySound.clip);
        yield return new WaitForSeconds(victorySound.clip.length);
        SceneManager.LoadScene("MainMenu");
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player.GetComponent<PlayerControls>().enabled = true;
    }

}
