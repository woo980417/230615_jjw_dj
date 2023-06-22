using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    void start()
    {
        Enemy e1 = new Enemy("elf", 10);
        Enemy e2 = new Enemy("dragon", 20);

        e1.GetEnemyinfo();
        e2.GetEnemyinfo();
    }
}
