using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private GameObject m_Enemy;
    [SerializeField] private List<GameObject> m_Players;
    private List<CharacterInstance> m_CharactersInstance;
    private CharacterInstance m_EnemyInstance;
    private int m_Turn;
    private int m_SelectedPlayer;

    private void Start()
    {
        m_Stack = new Stack<GameObject>();
        m_CharactersInstance = new List<CharacterInstance>();
        m_Turn = 0;

        m_CharactersInstance = GameManager.Instance.m_Party;
        m_EnemyInstance = GameManager.Instance.m_Fight.enemy;
        
        if (m_CharactersInstance.Count == 1) m_Players[1].SetActive(false);
        for (int i = 0; i < m_CharactersInstance.Count; i++)
        {
            m_Players[i].GetComponent<SpriteRenderer>().sprite = m_CharactersInstance[i].characterClass.stats.Sprite;
        }
        m_Enemy.GetComponent<SpriteRenderer>().sprite = m_EnemyInstance.characterClass.stats.Sprite;

        foreach (var menu in m_Buttons)
        {
            menu.SetActive(false);
        }
        m_EnemyInstance.LevelUp((int)GameManager.Instance.m_Fight.level);
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
            m_Stack.Peek().transform.GetChild(i+1).GetChild(0).GetComponent<TextMeshProUGUI>().text =
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
        m_EnemyInstance.HP -= m_CharactersInstance[m_SelectedPlayer].characterClass.skillUnlock[index].skill.damage;
        m_CharactersInstance[m_SelectedPlayer].Mana -= m_CharactersInstance[m_SelectedPlayer].characterClass.skillUnlock[index].skill.manaCost;
        PassTurn();
    }

    private void EnemyAttack()
    {
        int spell = UnityEngine.Random.Range(0, m_EnemyInstance.characterClass.skillUnlock.Count);
        int target = UnityEngine.Random.Range(0, m_CharactersInstance.Count);
        m_CharactersInstance[target].HP -= m_EnemyInstance.characterClass.skillUnlock[spell].skill.damage;
        m_EnemyInstance.Mana -= m_EnemyInstance.characterClass.skillUnlock[spell].skill.manaCost;
        PassTurn();
    }
}