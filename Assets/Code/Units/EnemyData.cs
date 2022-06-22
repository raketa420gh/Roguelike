using UnityEngine;

[CreateAssetMenu(menuName = "EnemyData", fileName = "EnemyData", order = 51)]
public class EnemyData : ScriptableObject
{
    public string PrefabPath;
    public string Name;
    public int MaxHealthPoints;
}