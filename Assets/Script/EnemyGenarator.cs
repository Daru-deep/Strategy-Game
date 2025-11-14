using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenarator : MonoBehaviour
{
    
    [SerializeField] public GameObject[] enemyPrefab;
    [SerializeField] public GameObject target;
    [SerializeField] public float generateInterval;
    [SerializeField] public bool start;
    private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] public Vector3 instancePos;
    
    public void CallCoroutine(string index,int count)
    {
        StartCoroutine(Generator(index,count));
    }

    IEnumerator Generator(string index,int count)//ここで、カウント分敵を生産
    {

            for(int i=0;i>count;i++)
            {
            
            yield return new WaitForSeconds(generateInterval);
            GameObject newEnemy = Instantiate(enemyPrefab[IDtoIndex(index)],instancePos, Quaternion.identity);
            
            enemies.Add(newEnemy);
            }
    }

    private int IDtoIndex(string index)//あとで、CSVのIDをIndexにすることで対応し、削除
    {

        
        if(index == "devil")
        {    
            return 0;
        }
        else if(index == "heavy")
        {
            return 1;   
        }
        else
        {
            return 0;
        }
        

    }
        

    public GameObject GetGameObject(int getnum)
    {
        return enemies[getnum];
        
    }


}
