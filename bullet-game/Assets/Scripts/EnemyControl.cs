using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public bool killLoop = false;
    public float shotDelay = 1.0f;
    public GameObject bullet;
    public Transform bulletSpawn;
    public GameObject particles;
    //public int health = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Run());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Run()
    {
        GameObject tempBullet;
        while (!killLoop)
        {
            yield return new WaitForSeconds(shotDelay);
            tempBullet = Instantiate(bullet);
            tempBullet.transform.position = bulletSpawn.position;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Entred Trigger");
        if(other.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);
            Instantiate(particles, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
