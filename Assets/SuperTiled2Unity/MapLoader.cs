using System.Collections;
using System.Collections.Generic;
using SuperTiled2Unity;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private SuperMap testMap;

    public GameObject currentMap;

    void Start()
    {
        LoadMap(testMap);
    }

    public void LoadMap(SuperMap map)
    {
        if (currentMap != null)
        {
            Destroy(currentMap);
        }

        currentMap = Instantiate(map.gameObject);

        SuperObject[] objects = currentMap.GetComponentsInChildren<SuperObject>();
        foreach (var superobj in objects)
        {
            if (superobj.m_Type.Contains("Portal"))
            {
                Collider2D col = superobj.GetComponent<Collider2D>();
                SuperCustomProperties props = superobj.GetComponent<SuperCustomProperties>();
                if (col.isTrigger = props.TryGetCustomProperty("Trigger", out CustomProperty prop))
                {
                    bool isTrigger = prop.GetValueAsBool();
                    col.isTrigger = isTrigger;
                }

                if (props.TryGetCustomProperty("Destination", out CustomProperty prop2))
                {
                    Debug.Log(prop2.GetValueAsString());
                }
            }
            else if (superobj.m_Type.Contains("Chest"))
            {
                GameObject chest = Resources.Load<GameObject>("Chest");
                Instantiate(chest, superobj.transform.position, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}