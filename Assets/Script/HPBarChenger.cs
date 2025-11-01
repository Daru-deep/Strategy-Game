using UnityEngine;
using UnityEngine.UI;

public class HPBarChenger : MonoBehaviour
{
    [SerializeField] private GameObject Green;
    private Image greenBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        greenBar = Green.GetComponent<Image>();
    }
    public void GreenChenge(float quotient)
    {
        greenBar.fillAmount = quotient;
    }
}
