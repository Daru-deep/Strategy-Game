using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GaugeSCO gaugeSCO;
    [SerializeField] private Slider aiSlider;
    [SerializeField] private Slider mpSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SynchronizationBar();
    }

    void SynchronizationBar()
    {
        float nowAiPoint = gaugeSCO.GetAiPoint();
        float nowMpPoint = gaugeSCO.GetMpPoint();
        aiSlider.value = nowAiPoint / 100;
        mpSlider.value = nowMpPoint / 100;

    }
    

    void UseAi(float use)
    {
        float nowAiPoint = gaugeSCO.GetAiPoint();
        gaugeSCO.SetAiPoint(nowAiPoint -= use);
    }
    void UseMP(float use)
    {
        float nowMpPoint = gaugeSCO.GetMpPoint();
        gaugeSCO.SetMpPoint(nowMpPoint -= use);
    }

    
}
