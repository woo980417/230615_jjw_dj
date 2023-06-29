using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour{
    [SerializeField ] private GameObject enemyPrefab;

    List<GameObject> enemyPool = new List<GameObject>();


    private void Start(){

        if(enemyPool.Count != 10)
        {
            GameObject obj = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity, this.transform);
            obj.transform.position = new Vector3(Random.Range(-3, 3), Random.Range(10, 16), 0);
            Enemy e = new Enemy(Enemy.EnemyType.Basic, 3, 10, Random.Range(1, 5));
            obj.GetComponent<EnemyObject>().SetEnemy(e);

            enemyPool.Add(obj);

        }
        else
        {
            for(int i = 0; i < enemyPool.Count; i++)
            {
                if(enemyPool[i] == null)
                {
                    enemyPool.RemoveAt(i);
                }
            }
        }

        /*for(int i = 0; i< 10; i++)
        {
            GameObject obj = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity, this.transform);
            obj.transform.position = new Vector3(Random.Range(-2,2), 5 + Random.Range(0,3), 0);
            Enemy e = new Enemy(Enemy.EnemyType.Basic, 100, 10, 0.5f+i);
            obj.GetComponent<EnemyObject>().SetEnemy(e);
        }
        */
    }


    // Update is called once per frame
    void Update(){


        //Instantiate()



        
    }
}
