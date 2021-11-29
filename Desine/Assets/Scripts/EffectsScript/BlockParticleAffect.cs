using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockParticleAffect : MonoBehaviour
{
    public ParticleSystem particleAffect;
    // Start is called before the first frame update
    void Start()
    {
        if (particleAffect.gameObject.activeSelf == true)
        {
            particleAffect.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator BlockEffect()
    {
        particleAffect.gameObject.SetActive(true);
        yield return new WaitForSeconds(particleAffect.startLifetime);
        particleAffect.gameObject.SetActive(false);
    }
}
