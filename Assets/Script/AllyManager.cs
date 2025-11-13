using Unity.VisualScripting;
using UnityEngine;

public class AllyManager : CharactorManager
{

    protected override void OnDestroy()
    {
        var gmobj = GameObject.FindWithTag("GameManager");
        if (gmobj != null)
        {
            var gm = gmobj.GetComponent<GameManager>();
            gm.PulsAi();
        }
        else
        {
          
            Debug.LogError("gmobjがNULLです！！");
        }

        
        StopAllCoroutines(); 
        Destroy(HPInstance);
        Destroy(gameObject);
    }
        

}
