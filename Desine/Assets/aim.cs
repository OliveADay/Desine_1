using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aim : MonoBehaviour
{
    public GameObject target;
    public bool targTag;
    public string tagT;
    Vector3 difference;
    float zRot;
    bool done = true;
    public GameObject projectile;
    public float addZRot;
    public int spawnInterval;
    // Start is called before the first frame update
    void Start()
    {
        if (targTag)
        {
            target = GameObject.FindGameObjectWithTag(tagT);
        }
    }

    // Update is called once per frame
    void Update()
    {
        difference = target.transform.position - transform.position;
        zRot = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRot));
        if (done)
        {
            StartCoroutine(Wait(spawnInterval));
        }
    }
    IEnumerator Wait(float time)
    {
        done = false;
        yield return new WaitForSeconds(time);
        GameObject p = Instantiate(projectile, transform.position, Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, zRot + addZRot));
        Projectile projp = p.GetComponent<Projectile>();
        projp.main = difference.normalized;
        done = true;
    }
}
