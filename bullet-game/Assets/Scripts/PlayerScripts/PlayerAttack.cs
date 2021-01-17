using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float shotDelay = .5f;
    public GameObject bullet;
    public Transform bulletSpawn;
    private GameObject tempBullet;
    private bool canFire = true;
    private float timer;

    public AudioSource source;

    private Transform objectHolder;

    private GameStateManager gsm;
    // Start is called before the first frame update
    void Start()
    {
        objectHolder = GameObject.FindGameObjectWithTag("ObjectHolder").transform;
        gsm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canFire && timer >0 && gsm.State == GameState.InLevel)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                canFire = true;
        }
        if (Input.GetButtonDown("Fire1") && canFire)
        {
            source.Play();
            tempBullet = Instantiate(bullet, objectHolder);
            tempBullet.transform.position = bulletSpawn.position;
            tempBullet.transform.rotation = bulletSpawn.rotation;
            canFire = false;
            timer = shotDelay;
        }
        
    }
}
