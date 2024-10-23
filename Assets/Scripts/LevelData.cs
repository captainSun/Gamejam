using UnityEngine;

/// <summary>
/// 关卡配置文件
/// </summary>
[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObject/关卡配置", order = 0)]
public class LevelData : ScriptableObject
{
    public Vector3 peopleStartPos;//人的起点坐标
    public Vector3 peopleStartRot; //人的起始角度
    public Vector3 dogStartPos;//狗的起点坐标
    public Vector3 dogStartRot; //狗的起始角度
    public float timeLimit;//时间限制
    public int hp;//起始生命值
}