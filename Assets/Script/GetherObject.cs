using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetherObject : MonoBehaviour
{
    float rangeDistance = 4f;
    Transform playerTransform;
    bool inRange = false, wasInRange = false, lockedConfirm = false;

    float objectDeleteYRange = -50;

    PlayerController thePlayerControl;
    GameManager manager;

    public Image imageRadar;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        thePlayerControl = GameObject.FindObjectOfType<PlayerController>();
        manager = GameObject.FindObjectOfType<GameManager>();

        Radar.RegisterRadarObject(this.gameObject, imageRadar);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if this object is close enough to Player
        if (Vector3.Distance(transform.position, playerTransform.position) <= rangeDistance)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        // If object close to player
        if (inRange && !lockedConfirm)
        {
            // Introduce this object
            thePlayerControl.getherObjectIsInRange(gameObject.tag);
            wasInRange = true;
            if (lockedConfirm)
            {
                //Debug.Log("Lock Success");
            }
        }
        else if (!inRange && wasInRange)
        {
            thePlayerControl.getherObjectIsNotInRange(gameObject.tag);
            wasInRange = false;
            lockedConfirm = false;
        }

        // Destroy object if fall from platform
        if(transform.position.y <= objectDeleteYRange)
        {
            Destroy(gameObject);
            manager.reduceObject();
        }
    }

    public void thisObjectTaken()
    {
        thePlayerControl.getherObjectIsNotInRange(gameObject.tag);
        wasInRange = false;

        Destroy(gameObject);
        manager.reduceObject();
    }

    public void LockThisObject()
    {
        lockedConfirm = true;
    }

    private void OnDestroy()
    {
        Radar.RemoveRadarObject(this.gameObject);
    }
}
