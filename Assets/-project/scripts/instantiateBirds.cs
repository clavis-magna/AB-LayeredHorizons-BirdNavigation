using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateBirds : MonoBehaviour
{
    public GameObject bird;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < 360; x = x + 15)
        {
            float angle = x + Random.Range(0, 15);
            float outSpeed = Random.Range(5.5f, 11.11f);
            float backSpeed = Random.Range(11.11f, 22.22f);
            GameObject newBird = Instantiate(bird, transform.position, Quaternion.identity);
            movementTest movementTestScript = newBird.GetComponent<movementTest>();
            movementTestScript.angleToFly = angle;
            movementTestScript.distancePerSecondOut = outSpeed;
            movementTestScript.distancePerSecondBack = backSpeed;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
