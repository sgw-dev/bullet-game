using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Route
    {
        public Transform[] path;
    }
    public Route[] paths;
    public GameObject basicEnemy;
    public float spawnDelay;
    public float waveDelay;
    public Transform spawn;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator StartGame() {
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(SpawnWave(0, basicEnemy, 5));
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(SpawnWave(1, basicEnemy, 5));
    }
    IEnumerator SpawnWave(int path, GameObject enemy, int number)
    {
        GameObject temp;
        for (int i = 0; i < number; i++)
        {
            temp = Instantiate(enemy, spawn);
            temp.GetComponent<FollowCurve>().setUp(paths[path].path);
            yield return new WaitForSeconds(waveDelay);
        }
        
    }
}
