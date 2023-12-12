using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Random = System.Random;

enum CombatMenus
{
    Actions,
    Players,
    Abilities
}

public class UI : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_Buttons;
    private Stack<GameObject> m_Stack;
    [SerializeField] private List<GameObject> m_Enemies;
    [SerializeField] private List<GameObject> m_Players;
    [SerializeField] private List<GameObject> m_Abilities;
    private List<CharacterInstance> m_CharactersInstance;
    private List<CharacterInstance> m_EnemiesInstance;
    private int m_Turn;
    private int m_SelectedPlayer;

    private void Start()
    {
        m_Stack = new Stack<GameObject>();
        m_CharactersInstance = new List<CharacterInstance>();
        m_Turn = 0;

        m_CharactersInstance = GameManager.Instance.m_Party;
        m_EnemiesInstance = GameManager.Instance.m_Fight.enemies;

        if (m_CharactersInstance.Count == 1) m_Players[1].SetActive(false);
        for (int i = 0; i < m_CharactersInstance.Count; i++)
        {
            m_Players[i].GetComponent<SpriteRenderer>().sprite = m_CharactersInstance[i].characterClass.stats.Sprite;
            m_Players[i].GetComponent<SpriteRenderer>().flipX = m_CharactersInstance[i].characterClass.stats.m_FlipX;
            m_Players[i].GetComponent<Animator>().runtimeAnimatorController = m_CharactersInstance[i].characterClass.stats.m_Animator;
        }

        if (m_EnemiesInstance.Count == 1) m_Enemies[1].SetActive(false);
        for (int i = 0; i < m_EnemiesInstance.Count; i++)
        {
            m_Enemies[i].GetComponent<SpriteRenderer>().sprite = m_EnemiesInstance[i].characterClass.stats.Sprite;
            if (GameManager.Instance.m_Fight.type != EnemyType.Boss)
            {
                m_EnemiesInstance[i].LevelUp((int)GameManager.Instance.m_Fight.level + (int)m_CharactersInstance[0].level);
                m_Enemies[i].GetComponent<SpriteRenderer>().flipX = m_EnemiesInstance[i].characterClass.stats.m_FlipX;
                m_Enemies[i].GetComponent<Animator>().runtimeAnimatorController = m_EnemiesInstance[i].characterClass.stats.m_Animator;
            }
            else
            {
                m_EnemiesInstance[i].LevelUp((int)GameManager.Instance.m_Fight.level);
            }
        }

        foreach (var menu in m_Buttons)
        {
            menu.SetActive(false);
        }

        UpdateUi();
        NextMenu(0);
    }

    public void ReturnToGame()
    {
        AudioMan._instance.Play("Button");
        SceneManager.LoadScene("WorldMap");
    }

    public void PassTurn()
    {
        m_Turn++;
        if (m_Turn % 2 == 1)
        {
            foreach (var menu in m_Buttons)
            {
                menu.SetActive(false);
                if (m_Stack.Count > 0) m_Stack.Pop();
            }

            EnemyAttack();
        }
        else
        {
            NextMenu(0);
        }
    }

    public void Attack()
    {
        NextMenu((int)CombatMenus.Players);
        if (m_CharactersInstance.Count == 1) m_Stack.Peek().transform.GetChild(2).gameObject.SetActive(false);
        for (int i = 0; i < m_CharactersInstance.Count; i++)
        {
            m_Stack.Peek().transform.GetChild(i + 1).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                m_CharactersInstance[i].characterClass.name;
        }
    }

    public void SelectPlayer(int index)
    {
        NextMenu((int)CombatMenus.Abilities);
        if (m_CharactersInstance[index].characterClass.skillUnlock.Count == 1)
        {
            m_Stack.Peek().transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            m_Stack.Peek().transform.GetChild(2).gameObject.SetActive(true);
        }

        for (int i = 0; i < m_CharactersInstance[index].characterClass.skillUnlock.Count; i++)
        {
            m_Stack.Peek().transform.GetChild(i + 1).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                m_CharactersInstance[index].characterClass.skillUnlock[i].name;
        }

        m_SelectedPlayer = index;
    }


    public void NextMenu(int index)
    {
        if (m_Stack.Count > 0) m_Stack.Peek().SetActive(false);
        m_Stack.Push(m_Buttons[index]);
        m_Buttons[index].SetActive(true);
    }

    public void LastMenu()
    {
        m_Stack.Peek().SetActive(false);
        m_Stack.Pop();
        if (m_Stack.Count > 0) m_Stack.Peek().SetActive(true);
    }

    public void Ability(int index)
    {
        int target = UnityEngine.Random.Range(0, m_EnemiesInstance.Count);
        
        m_EnemiesInstance[target].HP -= m_CharactersInstance[m_SelectedPlayer].characterClass.skillUnlock[index].skill.damage;
        m_CharactersInstance[m_SelectedPlayer].Mana -= m_CharactersInstance[m_SelectedPlayer].characterClass.skillUnlock[index].skill.manaCost;
        UpdateUi();
        Vector3 targetPos = m_Enemies[target].transform.position;
        GameObject currentVfx = Instantiate(m_CharactersInstance[m_SelectedPlayer].characterClass.skillUnlock[index].skill.vfx, targetPos, Quaternion.identity);
        currentVfx.GetComponent<ParticleSystem>().Play();
        AudioClip clip = m_CharactersInstance[m_SelectedPlayer].characterClass.skillUnlock[index].skill.sfx;
        
        PassTurn();
    }

    private void EnemyAttack()
    {
        int attacker = UnityEngine.Random.Range(0, m_EnemiesInstance.Count);
        int spell = UnityEngine.Random.Range(0, m_EnemiesInstance[attacker].characterClass.skillUnlock.Count);
        int target = UnityEngine.Random.Range(0, m_CharactersInstance.Count);
        m_CharactersInstance[target].HP -= m_EnemiesInstance[attacker].characterClass.skillUnlock[spell].skill.damage;
        m_EnemiesInstance[attacker].Mana -=
            m_EnemiesInstance[attacker].characterClass.skillUnlock[spell].skill.manaCost;
        UpdateUi();
        PassTurn();
    }

    private void UpdateUi()
    {
        for (int i = 0; i < m_EnemiesInstance.Count; i++)
        {
            m_Enemies[i].transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Slider>().value =
                m_EnemiesInstance[i].percentHP;
            m_Enemies[i].transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Slider>().value =
                m_EnemiesInstance[i].percentMana;
        }

        for (int i = 0; i < m_CharactersInstance.Count; i++)
        {
            m_Players[i].transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Slider>().value =
                m_CharactersInstance[i].percentHP;
            m_Players[i].transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Slider>().value =
                m_CharactersInstance[i].percentMana;
        }
    }
}