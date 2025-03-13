using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    public float InitialSpeed;
        
    public float SpeedIncreaseMultiplier;

    public float SyncDelay;
}