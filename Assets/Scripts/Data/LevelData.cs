using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    public float InitialSpeed;

    public float SpeedIncreaseMultiplier;

    public float SyncDelay = 0.5f;

    public int PointPerPickUp = 5;

    /// <summary>
    /// Points per 10 distance travelled
    /// </summary>
    [Tooltip("Points per 10 distance travelled")]
    public int PointDistance = 3;

    public float DistanceThresholdForPoint = 10;
}