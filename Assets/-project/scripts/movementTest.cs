using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementTest : MonoBehaviour
{
    /*
     * OUTGOING SPEEDS
     * between 20 & 40 km / hour
     * 20 km / hour scales 1:100 @ 200 meters / 36 seconds
     * = 5.55m / second
     * 40 km / hour scales 1:100 @ 400 meters / 36 seconds
     * = 11.11m / second
     * 
     * RETURNING SPEEDS
     * between 40 & 80 km / hour 
     * 40 km / hour scales 1:100 @ 400 meters / 36 seconds
     * = 11.11m / second     
     * 80 km / hour scales 1:100 @ 800 meters / 36 seconds
     * = 22.22m / second
    */

    float time = 1f; //seconds
    public float distancePerSecondOut = 11.11f;
    public float distancePerSecondBack = 22.22f;

    bool isFlyingOut = true;
    bool isFlyingHome = false;
    Vector3 startPos;

    public float angleToFly = 0;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0, angleToFly, 0);
        //set startpos
        startPos = transform.position;

        // fly out for x amount of hours
        Invoke("timesToFlyOut", scaleTime(3));

        print("starting flight out");
    }

    void Update()
    {
        if (isFlyingOut)
        {
            transform.Translate(Vector3.forward * (Time.deltaTime * (distancePerSecondOut / time)));
        }

        if (isFlyingHome)
        {
            transform.Translate(Vector3.forward * (Time.deltaTime * (distancePerSecondBack / time)));
            if(Vector3.Distance(transform.position, startPos) < 1)
            {
                isFlyingHome = false;
            }
        }
    }

    void timesToFlyOut()
    {
        print("staring to forage");
        isFlyingOut = false;

        // currently just waiting in the one place
        // but will build in some foraging

        // hang out (forage) for x amount of hours
        Invoke("endForage", scaleTime(5));
    }

    void endForage()
    {
        print("should retuurn home now");
        transform.LookAt(startPos);
        isFlyingHome = true;
    }

    float scaleTime(float hoursIn)
    {
        //hours * 60 minutes * 60 seconds / 100
        return ((hoursIn * 60 * 60) / 100);
    }

}
