using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "Character")]
public class EnemySCO : ScriptableObject
{
    [Header("Information")]
    public string charaname;
    public Sprite normalImage;
    public Sprite attackImage;
    public Sprite attackImage2;
    public Sprite downImage;
    public bool doFloat;
    public float fullHP;
    
    public float MPConsumption;
    public float walkSpeed;
    public GameObject target;
    public string targetTagName;//�^�[�Q�b�g�̃^�O��

    [Header("Attack")]
    public float attackRange;      // �U���͈�
    public float attackSpeed;   // �U���Ԋu�i�b�j
    public float attackPoint;
    public float moveStopDistance;
}
