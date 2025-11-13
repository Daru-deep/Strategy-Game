using UnityEngine;

[System.Serializable]
public class AllyEntry
{
    public string allyname;
    public GameObject prefab;
}


public class AllyGenerator : MonoBehaviour
{
    [SerializeField] private AllyEntry[] allis;


 
    [SerializeField] private Transform spawnPoint;


    [SerializeField] private GameManager gameManager;

    public void SpornAlly(int index)
    {
        if (index >= allis.Length || index < 0)//不正なindexのチェック
        {
            Debug.LogError($"不正なindex:{index}");
            return;
        }

        GameObject prefab = allis[index].prefab;
        CharactorManager cm = prefab.GetComponent<CharactorManager>();
        float cost = cm.GetConsumeMP();
        ConsumeMPpoints(cost);

         Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        

    }
    

    private void ConsumeMPpoints(float Consume)
    {
        gameManager.UseMP(Consume);
    }
}
