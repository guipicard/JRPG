using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    [SerializeField] private GameObject objToFollow;
    [SerializeField] private float mapMaxX;
    [SerializeField] private float mapMaxY;
    [SerializeField] private float mapMinX;
    [SerializeField] private float mapMinY;

    private Vector3 Pos;

    void Update()
    {
        if (transform.position.x >= mapMinX && transform.position.x <= mapMaxX && transform.position.y >= mapMinY && transform.position.y <= mapMaxY)
        {
            Pos = new Vector3(objToFollow.transform.position.x, objToFollow.transform.position.y, transform.position.z);
            transform.position = Pos;
        }
        if (transform.position.y > mapMaxY)
        {
            Pos = new Vector3(objToFollow.transform.position.x, mapMaxY, transform.position.z);
            transform.position = Pos;
        }
        if (transform.position.y < mapMinY)
        {
            Pos = new Vector3(objToFollow.transform.position.x, mapMinY, transform.position.z);
            transform.position = Pos;
        }
        if (transform.position.x > mapMaxX)
        {
            Pos = new Vector3(mapMaxX, objToFollow.transform.position.y, transform.position.z);
            transform.position = Pos;
            if(transform.position.y < mapMinY)
            {
                Pos = new Vector3(mapMaxX, mapMinY, transform.position.z);
                transform.position = Pos;
            }
            if (transform.position.y > mapMaxY)
            {
                Pos = new Vector3(mapMaxX, mapMaxY, transform.position.z);
                transform.position = Pos;
            }
        }
        if (transform.position.x < mapMinX)
        {
            Pos = new Vector3(mapMinX, objToFollow.transform.position.y, transform.position.z);
            transform.position = Pos;
            if (transform.position.y < mapMinY)
            {
                Pos = new Vector3(mapMinX, mapMinY, transform.position.z);
                transform.position = Pos;
            }
            if (transform.position.y > mapMaxY)
            {
                Pos = new Vector3(mapMinX, mapMaxY, transform.position.z);
                transform.position = Pos;
            }
        }
    }
}
