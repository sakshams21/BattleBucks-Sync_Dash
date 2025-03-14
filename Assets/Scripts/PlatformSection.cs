using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSection : MonoBehaviour
{
    [SerializeField] private Transform[] Obstacle_Positions;

    private List<GameObject> _pickups = new();
    private List<GameObject> _obstacles = new();

    public void SetupSection()
    {
        //to prevent 2 obstacle right after another
        bool obstaclePlaced = false;
        for (int i = 0; i < Obstacle_Positions.Length; i++)
        {
            if (UnityEngine.Random.Range(0, 2) == 1 && !obstaclePlaced)
            {//place obstacle
                var item = GameManager.Instance.GetObstacle();
                item.transform.SetParent(Obstacle_Positions[i]);
                item.gameObject.SetActive(true);
                item.transform.localPosition = Vector3.zero;
                obstaclePlaced = true;
                _obstacles.Add(item);
            }
            else
            {//place pickup
                var item = GameManager.Instance.GetPickup();
                item.transform.SetParent(Obstacle_Positions[i]);
                item.gameObject.SetActive(true);

                item.transform.GetChild(0).gameObject.SetActive(true);
                item.transform.GetChild(0).transform.localPosition = Vector3.zero;
                item.transform.GetChild(1).gameObject.SetActive(true);
                item.transform.GetChild(1).transform.localPosition = Vector3.zero;

                item.transform.localPosition = Vector3.zero;
                obstaclePlaced = false;
                _pickups.Add(item);
            }
        }

    }

    public void OnSectionDestroy()
    {
        foreach (var item in _obstacles)
        {
            GameManager.Instance.BackToPool(item, Type.obstacle);
        }
        _obstacles.Clear();
        foreach (var item in _pickups)
        {
            GameManager.Instance.BackToPool(item, Type.pickup);
        }
        _pickups.Clear();
    }

}