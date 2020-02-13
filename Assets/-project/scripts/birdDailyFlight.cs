using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdDailyFlight : MonoBehaviour
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
        // look in the correct direction my my arc
        transform.rotation = Quaternion.Euler(0, angleToFly, 0);
        //check if land within 80km (800m at 1:100 scale)
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 800))
        {
            if(hit.transform.tag == "island")
            {
                Destroy(this.gameObject);
            }
        }
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
            if (transform.position.y < 0.4f)
            {
                transform.position = transform.position + new Vector3(0, 0.001f, 0);
            }
        }

        if (isFlyingHome)
        {
            transform.Translate(Vector3.forward * (Time.deltaTime * (distancePerSecondBack / time)));

            // our distance check only cares about x,z (not height)
            if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(startPos.x, startPos.z)) < 1)
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
        // this should be 5. reduced to 0.5f for debugging
        Invoke("endForage", scaleTime(0.5f));
    }

    void endForage()
    {
        print("should retuurn home now");
        // look at home + 200m into the air (= 2m @ 1:100 scale)
        // returning home flying high to avoid frigate birds
        transform.LookAt(startPos + new Vector3(0,2,0));
        isFlyingHome = true;
    }

    float scaleTime(float hoursIn)
    {
        //hours * 60 minutes * 60 seconds / 100
        return ((hoursIn * 60 * 60) / 100);
    }

}
