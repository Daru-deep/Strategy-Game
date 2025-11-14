using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class AllyEntry
{
    public string allyname;
    public GameObject prefab;
}


public class AllyGenerator : MonoBehaviour
{
    [SerializeField] private AllyEntry[] allis;

    [SerializeField] private GaugeSCO gaugeSCO;
 
    [SerializeField] private Transform spawnPoint;


    [SerializeField] private GameManager gameManager;

    public void SpawanAlly(int index)
    {
        if (index >= allis.Length || index < 0)//不正なindexのチェック
        {
            Debug.LogError($"不正なindex:{index}");
            return;
        }

        GameObject prefab = allis[index].prefab;
        Quaternion prefabQ = prefab.transform.rotation;//prefabのRoateを獲得

        CharactorManager cm = prefab.GetComponent<CharactorManager>();
        
        float cost = cm.GetConsumeMP();

        Debug.Log($"SpawnAlly: MP={gaugeSCO.GetMpPoint()}, Cost={cost}");


        if(gaugeSCO.GetMpPoint() < cost)
        {
            Debug.Log($"<color = yellow>MPが足りません！！:必要MP{cost}</color>");
            return;
        }
        ConsumeMPpoints(cost);
        Instantiate(prefab, spawnPoint.position, prefabQ);
        

    }
    

    private void ConsumeMPpoints(float Consume)
    {
        gameManager.UseMP(Consume);
    }
}
