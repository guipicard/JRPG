using SuperTiled2Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private SuperMap testMap;
    private GameObject currentMap;
    private void Start()
    {
        LoadMap(testMap);
    }

    private void LoadMap(SuperMap map)
    {
        if(currentMap != null)
        {
            Destroy(currentMap);
        }

        currentMap = Instantiate(map).gameObject;

        SuperObject[] objects = currentMap.GetComponentsInChildren<SuperObject>();
        foreach(SuperObject obj in objects) 
        {
            if(obj.m_Type.Contains(""))
            {
                //do something
            }
            else if (obj.m_Type.Contains("Spawn"))
            {
                //Instantiate(playerPrefab, obj.transform.position, Quaternion.identity);
            }
        }
    }
}
