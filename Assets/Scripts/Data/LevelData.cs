using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    public float InitialSpeed;

    public float SpeedIncreaseMultiplier;

    public float SyncDelay = 0.5f;

    public int TotalLives = 10;

    public int ScorePerCollectablePickup;

    /// <summary>
    /// Points per 10 distance travelled
    /// </summary>
    [Tooltip("Points per 10 distance travelled")]
    public int ScorePerDistanceTravelled;

    public float DistanceThresholdForPoint;
}