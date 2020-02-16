using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public Character[] LeftSide;
    public Character[] RightSide;

    public GameObject LeftUI;
    public GameObject RightUI;

    private bool _waitForClick = false;

    public void Start()
    {
        StartCoroutine(GameLoop());
    }

    public void Init()
    {
        StopAllCoroutines();

        foreach (var c in LeftSide)
        {
            c.Init();
        }

        foreach (var c in RightSide)
        {
            c.Init();
        }

        Start();
    }

    public void AttackSomeone()
    {
        if (_waitForClick)
        {
            Debug.Log("AttackSomeone");
            _waitForClick = false;
        }
        else
        {
            Debug.Log("Impossible state");
        }
    }

    private void UpdateUI(bool visible, bool leftSide)
    {
        if (visible)
        {
            _waitForClick = true;
        }
        if (LeftUI != null)
        {
            LeftUI.SetActive(visible && leftSide);
        }
        if (RightUI != null)
        {
            RightUI.SetActive(visible && !leftSide);
        }
    }

    private void PlayerLost()
    {
        Debug.Log("PlayerLost");
    }

    private void PlayerWin()
    {
        Debug.Log("PlayerWin");
    }

    private IEnumerator GameLoop()
    {
        bool leftTurn = true;
        UpdateUI(false, false);

        while (true)
        {
            // Found alive characters
            var leftAlive = LeftSide.FirstOrDefault(a => !a.IsDead);
            var rightAlive = RightSide.FirstOrDefault(a => !a.IsDead);
            bool leftDead = leftAlive == null;
            bool rightDead = rightAlive == null;

            // check win/lost conditions
            if (leftDead)
            {
                PlayerLost();
                yield break;
            }

            if (rightDead)
            {
                PlayerWin();
                yield break;
            }

            // show button and wait for click
            UpdateUI(true, leftTurn);
            while (_waitForClick)
            {
                yield return null;
            }
            // Hide UI
            UpdateUI(false, false);

            // make attack
            Character c1, c2;
            if (leftTurn)
            {
                c1 = leftAlive;
                c2 = rightAlive;
            }
            else
            {
                c1 = rightAlive;
                c2 = leftAlive;
            }

            c1.Attack(c2.gameObject);

            // wait for attack complete
            while (!c1.IsIdle)
            {
                yield return null;
            }

            // switch side
            leftTurn = !leftTurn;

            yield return null;
        }
    }
}
