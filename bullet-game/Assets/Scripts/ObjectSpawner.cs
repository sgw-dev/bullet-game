using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public int width;
    public int length;
    public int maxTrees;
    public int maxRocks;
    public int speed;

    public float delay;
    public bool canTree = true;
    public bool canRock = true;

    public GameObject[] trees;
    public GameObject[] rocks;

    private int numTrees = 0;
    private int numRocks = 0;

    private BoxCollider box;
    private Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(numTrees < maxTrees && canTree)
        {
            int index = Random.Range(0, trees.Length);
            Grow(trees[index]);
            numTrees++;
            canTree = false;
            StartCoroutine("CoolDown", "canTree");
        }
        if (numRocks < maxRocks && canRock)
        {
            int index = Random.Range(0, rocks.Length);
            Grow(rocks[index]);
            numRocks++;
            canRock = false;
            StartCoroutine("CoolDown", "canRock");
        }
    }
    private IEnumerator CoolDown(string flag)
    {
        yield return new WaitForSeconds(Random.Range(delay - .25f, delay + .25f));
        if(flag == "canTree")
        {
            canTree = !canTree;
        }
        else
        {
            canRock = !canRock;
        }
    }
    private void Grow(GameObject thing)
    {
        float startPos = Random.Range(transform.position.x - (width / 2), transform.position.x + (width / 2));
        GameObject temp = Instantiate(thing, new Vector3(startPos, transform.position.y, transform.position.z), Quaternion.Euler(0,Random.Range(0,360),0));
        box = temp.AddComponent<BoxCollider>();
        box.isTrigger = true;
        body = temp.AddComponent<Rigidbody>();
        body.useGravity = false;
        body.velocity = new Vector3(0, 0, -speed);
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Tree")
        {
            Destroy(other.gameObject);
            numTrees--;
        }else if(other.tag == "Rock")
        {
            Destroy(other.gameObject);
            numRocks--;
        }
    }
    public void OnDrawGizmos()
    {
        Vector3 topLeft = new Vector3(transform.position.x + (width / 2), transform.position.y, transform.position.z);
        Vector3 topRight = new Vector3(transform.position.x - (width / 2), transform.position.y, transform.position.z);
        Vector3 bottomLeft = new Vector3(transform.position.x + (width / 2), transform.position.y, transform.position.z-length);
        Vector3 bottomRight = new Vector3(transform.position.x - (width / 2), transform.position.y, transform.position.z- length);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(bottomLeft, bottomRight);
    }
}
