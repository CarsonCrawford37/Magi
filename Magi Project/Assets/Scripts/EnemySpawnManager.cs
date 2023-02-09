using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemy;
    public bool enemyDied;

    public ParticleSystem ps;
    private float timeToWait = 5f;

    void Update()
    {
        enemyDied = enemy.GetComponent<HealthComponent>().hasDied;
        //if (enemy.activeInHierarchy == false)
        //{

        //}

        if (enemyDied == true && enemy.activeInHierarchy == false) //
        {
            enemyDied = false;
            StartCoroutine(RespawnSpawnEnemy());

        }

    }

    IEnumerator RespawnSpawnEnemy()
    {
        //enemyDied = false;
        //Debug.Log("Im being called");
        yield return new WaitForSeconds(5);

        enemy.SetActive(true);
        ParticleSystem psClone = Instantiate(ps, enemy.transform.position, Quaternion.Euler(0, 90, 0));
        Destroy(psClone.gameObject, 5);
        StopAllCoroutines();
    }

}
