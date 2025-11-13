using UnityEngine;

public class AllyGenerator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject saber;
    [SerializeField] private Vector3 SpornPoint;
    [SerializeField] private GameObject SpornOB;

    [SerializeField] private GameManager gameManager;

    private void AllyGenerat(GameObject chara)
    {
        SpornPoint = SpornOB.transform.position;
        Instantiate(chara, SpornPoint, Quaternion.identity);
        ConsumeMPpoints(saber.GetComponent<AllyManager>().GetConsumeMP());
    }
    public void SaberGenerat()
    {
        
        AllyGenerat(saber);
    }
    
    private void ConsumeMPpoints(float Consume)
    {
        gameManager.UseMP(Consume);
    }
}
