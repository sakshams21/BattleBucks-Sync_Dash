using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player Ref_Player;
    public LevelData Ref_LevelData;

    [Space(15f)]
    [Header("Manager Referencces")]
    [SerializeField] private PlatformSectionManager Ref_PlatformManager;
    [SerializeField] private UI_Manager Ref_UI_Manager;
    [SerializeField] private PickUpManager Ref_PickupManager;
    [SerializeField] private ObstacleManager Ref_ObstacleManager;

    [SerializeField] private float NextSpawnDistance = 29f;

    public event Action<int> OnScoreUpdate;

    private int _startingSectionNumber = 3;

    private float _maxDistaceforOriginReset = 100;

    private bool originResetSoon = false;

    private Vector3 _nextSectionPosition;

    private int _score;

    public bool isGamePaused { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        Instance = this;

        _nextSectionPosition = new Vector3(0, -1, -NextSpawnDistance);

    }
    private IEnumerator Start()
    {
        Ref_UI_Manager.StartLoading();
        //generate pool
        yield return StartCoroutine(Ref_PlatformManager.GeneratePool(10));
        yield return StartCoroutine(Ref_PickupManager.GeneratePool(20));
        yield return StartCoroutine(Ref_ObstacleManager.GeneratePool(20));

        StartingSectionSpawn();
        Ref_UI_Manager.UnLoading();
    }
    #region Score Updates 

    public void ScoreAdd_Pickup()
    {
        _score += Ref_LevelData.PointPerPickUp;
        OnScoreUpdate?.Invoke(_score);
    }

    public void ScoreUpdateDistance()
    {

    }
    #endregion

    public void PauseGame(bool isPaused)
    {
        isGamePaused = isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void StartingSectionSpawn()
    {
        for (int i = 0; i < _startingSectionNumber; i++)
        {
            SpawnNextSection();
        }
    }

    public void PlayerDistanceCheck(float value)
    {
        originResetSoon = value > _maxDistaceforOriginReset;
    }


    public void SpawnNextSection()
    {
        var section = Ref_PlatformManager.GetItem();
        _nextSectionPosition.z += NextSpawnDistance;
        section.transform.position = _nextSectionPosition;

        section.SetupSection();
    }

    public void BackToPool()
    {
        Ref_PlatformManager.BackToPool(null);
        if (originResetSoon)
        {
            OriginReset();
        }
    }


    public void OriginReset()
    {
        //distance between the section its on and the current player
        var distances = Ref_PlatformManager.OriginReset(NextSpawnDistance);
        Ref_Player.OriginReset(distances.Item1);
        originResetSoon = false;
        _nextSectionPosition = distances.Item2;
    }

    public GameObject GetPickup()
    {
        return Ref_PickupManager.GetItem();
    }

    public GameObject GetObstacle()
    {
        return Ref_ObstacleManager.GetItem();
    }

    public void BackToPool(GameObject item, Type type)
    {
        if (type == Type.obstacle)
        {
            Ref_ObstacleManager.BackToPool(item);
        }
        else
        {
            Ref_PickupManager.BackToPool(item);
        }
    }

}

public enum Type
{
    obstacle,
    pickup
}