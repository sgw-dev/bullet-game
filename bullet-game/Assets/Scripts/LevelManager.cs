using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Route
    {
        public string name;
        public Transform[] path;
    }
    private Route[] paths;
    public GameObject[] pathObjects;
    public GameObject basicEnemy;
    public float spawnDelay;
    public float waveDelay;
    public Transform spawn;
    public float rotateStep = 0.005f;
    public float rotateDelay = 0.05f;
    public Transform cameraTop;
    public Transform cameraSide;
    // Start is called before the first frame update
    void Start()
    {
        paths = new Route[pathObjects.Length];
        for(int i = 0; i< pathObjects.Length; i++)
        {
            Route temp = new Route();
            int children = pathObjects[i].transform.childCount;
            temp.path = new Transform[children];
            for(int j = 0; j<children; j++)
            {
                temp.path[j] = pathObjects[i].transform.GetChild(j).transform;
            }
            paths[i] = temp;
        }
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
        yield return new WaitForSeconds(spawnDelay*2);
        StartCoroutine(ToSide());
        yield return new WaitForSeconds(spawnDelay);
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
    IEnumerator ToSide() {
        
        for (float i = 0; i<= 1; i+=rotateStep)
        {
            transform.rotation = Quaternion.Lerp(cameraTop.rotation, cameraSide.rotation, i*i);
            transform.position = Vector3.Slerp(cameraTop.position, cameraSide.position, i);
            yield return new WaitForSeconds(rotateDelay);
        }
        transform.rotation = Quaternion.Lerp(cameraTop.rotation, cameraSide.rotation, 1);
        transform.position = Vector3.Slerp(cameraTop.position, cameraSide.position, 1);

        /*Transform parent = this.transform.parent;
        for (int i = 0; i < 90 / rotateStep; i++)
        {
            parent.Rotate(new Vector3(0, -rotateStep, 0));
            yield return new WaitForSeconds(.05f);
        }
        for (int i = 0; i < 90/rotateStep; i++)
        {
            parent.Rotate(new Vector3(0, -rotateStep, -rotateStep));
            yield return new WaitForSeconds(.05f);
        }*/


    }
}
