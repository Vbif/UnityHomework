using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    public GameObject PoliceMan;
    public GameObject Hooligan;
    public GameObject Zombie;

    private void InitOne(GameObject o)
    {
        if (o != null)
        {
            var c = o.GetComponentInChildren<Character>();
            if (c != null)
            {
                c.Init();
            }
        }

        
    }

    public void Init()
    {
        InitOne(PoliceMan);
        InitOne(Hooligan);
        InitOne(Zombie);
    }

    private void AttackOne(GameObject o1, GameObject o2)
    {
        if (o1 != null && o2 != null)
        {
            var c = o1.GetComponentInChildren<Character>();
            if (c != null)
            {
                c.Attack(o2);
            }
        }
    }

    public void AttackHooligan()
    {
        AttackOne(PoliceMan, Hooligan);
    }

    public void AttackPoliceMan0()
    {
        AttackOne(Hooligan, PoliceMan);
    }

    public void AttackPoliceMan1()
    {
        AttackOne(Zombie, PoliceMan);
    }
}
