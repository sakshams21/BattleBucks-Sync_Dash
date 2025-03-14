using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour, IManager<GameObject>
{
    [SerializeField] private GameObject Pickup_Prefab;
    [SerializeField] private Transform PickupSpawn_Transform;
    private Queue<GameObject> _pickups_Pool_Disabled = new Queue<GameObject>();

    private WaitForSeconds _spawnWaitTime = new WaitForSeconds(0.05f);

    public IEnumerator GeneratePool(int maxPoolCount)
    {
        for (int i = 0; i < maxPoolCount; i++)
        {
            GameObject secton = Instantiate(Pickup_Prefab, PickupSpawn_Transform);
            secton.gameObject.SetActive(false);
            _pickups_Pool_Disabled.Enqueue(secton);
            yield return _spawnWaitTime;
        }
    }




    public GameObject GetItem()
    {
        GameObject section = (_pickups_Pool_Disabled.Count > 0) ? _pickups_Pool_Disabled.Dequeue() : Instantiate(Pickup_Prefab, PickupSpawn_Transform);
        section.gameObject.SetActive(true);
        return section;
    }

    public void BackToPool(GameObject pickup)
    {
        pickup.gameObject.SetActive(false);
        pickup.transform.GetChild(0).gameObject.SetActive(true);
        pickup.transform.GetChild(0).transform.position = Vector3.zero;
        pickup.transform.GetChild(1).gameObject.SetActive(true);
        pickup.transform.GetChild(1).transform.position = Vector3.zero;

        pickup.transform.SetParent(PickupSpawn_Transform);
        pickup.transform.position = Vector3.down;
        _pickups_Pool_Disabled.Enqueue(pickup);
    }


}