using UnityEngine;

[CreateAssetMenu(fileName = "GaugeSCO", menuName = "Scriptable Objects/GaugeSCO")]
public class GaugeSCO : ScriptableObject
{
    [SerializeField]private float aiPoint = 0;
    [SerializeField]private float mpPoint = 50;

    public void SetAiPoint(float setnum)
    {
        aiPoint = setnum;
    }

    public float GetAiPoint()
    {
        return aiPoint;
    }

    public void SetMpPoint(float setnum)
    {
        mpPoint = setnum;
    }

    public float GetMpPoint()
    {
        return mpPoint;
    }
}
