using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoliceSpawner : MonoBehaviour
{

    [System.Serializable]
    public class Wave
    {
        public string name;
        public int[] number;
        public GameObject[] enemy;
    }

    public Transform[] spawns;

    int currentWave = 0;
    public Wave[] waves;
    public Text text;


    // Start is called before the first frame update
    void Start()
    {
        SpawnNextWave();
        InvokeRepeating("EnemiesDefeated", 10f, 10f);
    }

    public void EnemiesDefeated()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length == 0)
        {
            SpawnNextWave();
        }
    }

    public void SpawnNextWave()
    {
        if(currentWave < waves.Length)
        {
            text.text = "Wave " + (currentWave + 1) + "/400" + " " + waves[currentWave].name;
            StartCoroutine(Spawn(waves[currentWave]));
            currentWave += 1;
        }
    }

    IEnumerator Spawn(Wave wave)
    {
        for(int i=0; i< wave.number.Length; i++)
        {
            for(int j=0; j<wave.number[i]; j++)
            {
               int number = Random.Range(0, spawns.Length);
               Instantiate(wave.enemy[i],spawns[number].position,spawns[number].rotation);
               yield return new WaitForSeconds(1f);
            }
        }
    }
}
