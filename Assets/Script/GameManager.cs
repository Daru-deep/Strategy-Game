using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("IS THIS TEST?")]
    [SerializeField] public bool thisIsTest; 
    [SerializeField] private GaugeSCO gaugeSCO;
    [SerializeField] private Slider aiSlider;
    [SerializeField] private Slider mpSlider;



    [SerializeField]private float AIPointsIncrease = 4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

    }
    void Awake()
    {
        if(thisIsTest)
        {
            ResetGaugeForTest();
        }
        SynchronizationBar();
    }

    public void ResetGaugeForTest()
    {
        gaugeSCO.SetAiPoint(20);
        gaugeSCO.SetMpPoint(100);
    }

    void SynchronizationBar()
    {
        float nowAiPoint = gaugeSCO.GetAiPoint();
        float nowMpPoint = gaugeSCO.GetMpPoint();
        aiSlider.value = nowAiPoint / 100;
        mpSlider.value = nowMpPoint / 100;

    }
    public void PulsAi()
    {
        float nowAiPoint = gaugeSCO.GetAiPoint();
        gaugeSCO.SetAiPoint(nowAiPoint += AIPointsIncrease);
        SynchronizationBar();
    }

    public void UseAi(float use)
    {
        float nowAiPoint = gaugeSCO.GetAiPoint();
        gaugeSCO.SetAiPoint(nowAiPoint -= use);
        SynchronizationBar();
    }
    public void UseMP(float use)
    {
        float nowMpPoint = gaugeSCO.GetMpPoint();

        gaugeSCO.SetMpPoint(nowMpPoint -= use);
        

        SynchronizationBar();
    }

    
}
