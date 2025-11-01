using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "Character")]
public class EnemySCO : ScriptableObject
{
    public string charaname;
    public Sprite normalImage;
    public Sprite attackImage;
    public Sprite attackImage2;
    public Sprite downImage;
    public bool doFloat;
    public float fullHP;
   
    public float walkSpeed;
    public GameObject target;
    public string targetTagName;//ƒ^[ƒQƒbƒg‚Ìƒ^ƒO–¼

    [Header("Attack")]
    public float attackRange;      // UŒ‚”ÍˆÍ
    public float attackSpeed;   // UŒ‚ŠÔŠui•bj
    public float attackPoint;
    public float moveStopDistance;
}
