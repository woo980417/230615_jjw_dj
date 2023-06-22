using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    private string name;
    private int hp;

    public Enemy(string name, int hp) // ������ -> ���� �� �ʿ䰡 ����.
    {
        this.name = name;
        this.hp = hp;
    }

    public void GetEnemyinfo()
    {
        Debug.Log($"name : {this.name} attack : {this.hp}");
    }
    
}
