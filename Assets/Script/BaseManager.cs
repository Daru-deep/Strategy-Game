using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BaseManager : MonoBehaviour
{
    [SerializeField] private Image HPBar;

    [SerializeField] private float HP = 100;
    [SerializeField] private float HPMax = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChengeHPBar();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void ChengeHP(float num)
    {
        HP += num;
        Debug.Log("ChengeHP");
        ChengeHPBar();
    }

    private void ChengeHPBar()
    {
        HP = Mathf.Clamp(HP, 0, HPMax);
        float HPAverage;
        HPAverage = HP / HPMax;
        if (HPAverage >= 0)
        {
            HPBar.fillAmount = HPAverage;
            
        }
        else
        {
            Debug.Log("HPAverage‚ª0ˆÈ‰º‚Å‚·II"+HPAverage);
        }

    }
}
