using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameManager manager;

    GameObject lockedObject;
    bool ObjectInRange = false, mouseIsUp = true;

    public GameObject ambilTumbuhanUi;
    public Text ambilTumbuhanText;

    public static bool animationPicking = false;
    public Animator animator;
    bool animationRun = false;

    // Start is called before the first frame update
    void Start()
    {
        lockedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ObjectInRange && mouseIsUp && !ThirdPersonMovement.playerIsMoving)
        {
            //FindObjectOfType<AudioManager>().Play("AmbilBarang");
            takeObject();
            mouseIsUp = false;
            animator.SetBool("Pick", true);
            animationPicking = true;
            animationRun = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseIsUp = true;
        }

        if (animationRun)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
            {
                animator.SetBool("Pick", false);
                animationRun = false;
                animationPicking = false;
            }
        }
    }

    public void getherObjectIsInRange(string name)
    {
        if(lockedObject == null)
        {
            lockedObject = FindClosestObject(name);
            SetActivationGetherText(name, true);
            ObjectInRange = true;
            // Lock this object
            lockedObject.GetComponent<GetherObject>().LockThisObject();
        }
    }

    public void getherObjectIsNotInRange(string name)
    {
        lockedObject = null;
        SetActivationGetherText(name, false);
        ObjectInRange = false;
    }

    public void takeObject()
    {
        GetherObject temp;
        temp = lockedObject.GetComponent<GetherObject>();

        temp.thisObjectTaken();

        manager.CheckObjectTaken(temp.tag);
    }

    // Find closest Object
    GameObject FindClosestObject(string name)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(name);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    // Set gether text to active
    void SetActivationGetherText(string name, bool set)
    {
        if (set)
        {
            ambilTumbuhanText.text = "Ambil " + name;
        }
        ambilTumbuhanUi.SetActive(set);
    }
}
