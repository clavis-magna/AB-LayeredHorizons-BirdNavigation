using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateLocationCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int layerMask = 1 << 8;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 480.0f / 2, layerMask);

        if (hitColliders.Length > 1)
        {
            print("destroying: " + hitColliders[0].transform.name);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void check()
    {

    }
}
