using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncPlayer : MonoBehaviour
{
    public static SyncPlayer Instance { get; private set; }

    [SerializeField] private Transform Ghost_Player;

    private float _lag;

    private Queue<(float time, Vector3 position)> positionHistory = new Queue<(float, Vector3)>();


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        _lag = GameManager.Instance.Ref_LevelData.SyncDelay;
    }

    public void ReceivePlayerData(Vector3 position)
    {
        positionHistory.Enqueue((Time.time, position));
    }
    private void Update()
    {
        float currentTime = Time.time;


        while (positionHistory.Count > 1 && currentTime - positionHistory.Peek().time > _lag)
        {
            positionHistory.Dequeue();
        }


        if (positionHistory.Count >= 2)
        {
            var enumerator = positionHistory.GetEnumerator();
            enumerator.MoveNext();
            var first = enumerator.Current; // Older position
            enumerator.MoveNext();
            var second = enumerator.Current; // Newer position

            float timeDiff = second.time - first.time;
            if (timeDiff > 0)
            {
                float t = (_lag - (currentTime - first.time)) / timeDiff;
                Ghost_Player.position = Vector3.Lerp(first.position, second.position, Mathf.Clamp01(t));
            }
        }
    }
}
