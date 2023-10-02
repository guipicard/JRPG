using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SaveData saveData;

   

    public void Save()
    {
        saveData.playerInWorldPosition = transform.position;
    }
    public void Load()
    {
        transform.position = saveData.playerInWorldPosition;
    }
    void Start()
    {
        GameManager.Instance.onSave += Save;
        GameManager.Instance.onLoad += Load;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D)) 
        {
            Vector2 pos = transform.position;

            pos.x += 10.0f * Time.deltaTime;

            transform.position = pos;
        }
    }
}
