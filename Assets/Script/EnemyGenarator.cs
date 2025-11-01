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
    
    private void Start()
    {
        StartCoroutine(Generator());
    }

    IEnumerator Generator()
    {

        while (start)
        {
            yield return new WaitForSeconds(generateInterval);
            int rand =  Random.Range(0,enemyPrefab.Length);
            GameObject newEnemy = Instantiate(enemyPrefab[rand],instancePos, Quaternion.identity);
            
            enemies.Add(newEnemy);
        }
    }

    public GameObject GetGameObject(int getnum)
    {
        return enemies[getnum];
    }


}
