using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DumbBossAI : MonoBehaviour
{
    // Start is called before the first frame update
    public int bossHealth = 10;
    public int bossDamamedByHp = 1;
    public int secondsBetweenAttackSeries = 5;
    public int secondsBetweenAttacks = 2;
    public int numberOfAttacks = 5;
    public float deathDelay = 1;
    public List<Vector2> spawnPoints = new List<Vector2>();
    public UnityEvent bossDead;
    public GameObject bossBullet;

    private bool seriesStarted = false;
    AudioSource battleStartedSound;
    AudioSource bossDamagedSound;
    AudioSource bossDefeatedSound;
    private Vector2 currentAttackSpawn; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bossHealth <= 0)
        {
            bossDead.Invoke();
            StartCoroutine(bossDying());
            Destroy(gameObject);
        }

        if (seriesStarted)
        {
            seriesStarted = false;
            StartCoroutine(DecisionMakingTime());
        }

    }


    IEnumerator bossDying()
    {
        bossDefeatedSound.PlayOneShot(bossDefeatedSound.clip);
        yield return new WaitForSeconds(deathDelay);
        bossDead.Invoke();
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "pl_projectile")
        {
            bossHealth -= bossDamamedByHp;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pl_projectile")
        {
            bossDamagedSound.PlayOneShot(bossDamagedSound.clip);
            bossHealth -= bossDamamedByHp;
            Destroy(collision.gameObject);
        }
    }


    IEnumerator DecisionMakingTime()
    {
        yield return new WaitForSeconds(secondsBetweenAttackSeries);
        StartCoroutine(AttackSequence());

    }

    IEnumerator AttackSequence()
    {
        for (int i=0; i < numberOfAttacks; i++)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Count);
            currentAttackSpawn = spawnPoints[spawnPointIndex];
            Instantiate(bossBullet, currentAttackSpawn, new Quaternion());
            yield return new WaitForSeconds(secondsBetweenAttacks);
        }
        seriesStarted = true;
    }

    public void battleStarted()
    {
        battleStartedSound.PlayOneShot(battleStartedSound.clip);
    }

}
