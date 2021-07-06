using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarObject
{
    public Image icon { get; set; }
    public GameObject owner { get; set; }
}

public class Radar : MonoBehaviour
{
    public Transform playerPos;
    float mapScale = 1.3f;
    float screenWidthDefault = 916;
    public GameObject arrow;
    public GameObject compass;

    public static List<RadarObject> radObjects = new List<RadarObject>();

    public static void RegisterRadarObject(GameObject o, Image i)
    {
        Image image = Instantiate(i);
        radObjects.Add(new RadarObject() { owner = o, icon = image });
    }

    public static void RemoveRadarObject(GameObject o)
    {
        List<RadarObject> newList = new List<RadarObject>();
        for(int i = 0; i < radObjects.Count; i++)
        {
            if (radObjects[i].owner == o)
            {
                Destroy(radObjects[i].icon);
                continue;
            }
            else
            {
                newList.Add(radObjects[i]);
            }
        }

        radObjects.RemoveRange(0, radObjects.Count);
        radObjects.AddRange(newList);
    }

    void DrawRadarDots()
    {
        foreach(RadarObject ro in radObjects)
        {
            if (ro.owner != null)
            {
                Vector3 radarPos = (ro.owner.transform.position - playerPos.position);
                float distToObject = Vector3.Distance(playerPos.position, ro.owner.transform.position) * mapScale;
                float deltay = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg - 270 - playerPos.eulerAngles.y;
                radarPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad) * -1;
                radarPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);

                ro.icon.transform.SetParent(this.transform);
                ro.icon.transform.position = new Vector3(radarPos.x, radarPos.z, 0) + this.transform.position;

                // Compass
                if (ro.owner.tag == "House")
                {
                    float distance = CalculateDistance(arrow.transform.position.x, arrow.transform.position.y, ro.icon.transform.position.x, ro.icon.transform.position.y);
                    if (distance > screenWidthDefault / 15.26)
                    {
                        compass.SetActive(true);
                        Vector3 dir = arrow.transform.position - ro.icon.transform.position; //Debug.Log(ro.icon.transform.position);
                        float newDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                        compass.transform.rotation = Quaternion.Euler(0f, 0f, newDirection + 90);
                    }
                    else
                    {
                        compass.SetActive(false);
                    }
                }
            }
        }
    }

    float CalculateDistance(float x1, float y1, float x2, float y2)
    {
        float result = Mathf.Sqrt(Mathf.Pow(x1 - x2, 2) + Mathf.Pow(y1 - y2, 2));
        return result;
    }

    private void Update()
    {
        DrawRadarDots();

        // Screen Correction
        if (Screen.width > screenWidthDefault)
        {
            float temp = Screen.width - screenWidthDefault;
            mapScale += (temp / 43.428f) * 0.1f;
            screenWidthDefault = Screen.width;
        }
        else if (Screen.width < screenWidthDefault)
        {
            float temp = screenWidthDefault - Screen.width;
            mapScale -= (temp / 43.428f) * 0.1f;
            screenWidthDefault = Screen.width;
        }
    }
}
