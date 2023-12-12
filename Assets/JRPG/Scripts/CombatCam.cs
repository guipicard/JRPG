using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCam : MonoBehaviour
{
    [SerializeField] private GameObject map;

    void Update()
    {
        this.transform.position = map.transform.position;
    }
}
