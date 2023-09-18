using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTester : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) {
            m_animator.SetTrigger("Atk1");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            m_animator.SetTrigger("Atk2");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            m_animator.SetTrigger("Atk3");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            m_animator.SetTrigger("AtkUlti");
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            m_animator.SetTrigger("TakeDamage");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            m_animator.SetTrigger("Die");
        }
    }
}
