using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour, IManager<GameObject>
{
    [SerializeField] private GameObject Obstacle_Prefab;
    [SerializeField] private Transform ObstacleSpawn_Transform;
    private Queue<GameObject> _obstacle_Pool = new Queue<GameObject>();
    public int MaxPoolCount = 10;
    private WaitForSeconds _spawnWaitTime = new WaitForSeconds(0.05f);
    public IEnumerator GeneratePool(int maxPoolCount)
    {
        for (int i = 0; i < maxPoolCount; i++)
        {
            GameObject secton = Instantiate(Obstacle_Prefab, ObstacleSpawn_Transform);
            secton.gameObject.SetActive(false);
            _obstacle_Pool.Enqueue(secton);
            yield return _spawnWaitTime;
        }
    }
    public GameObject GetItem()
    {
        return _obstacle_Pool.Dequeue();
    }

    public void BackToPool(GameObject obj)
    {
        obj.transform.SetParent(ObstacleSpawn_Transform);
        obj.SetActive(false);
        _obstacle_Pool.Enqueue(obj);
    }


}