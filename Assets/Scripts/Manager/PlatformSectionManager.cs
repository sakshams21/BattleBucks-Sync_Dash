using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSectionManager : MonoBehaviour, IManager<PlatformSection>
{
  [SerializeField] private PlatformSection Platform_Prefab;
  [SerializeField] private Transform SectionSpawn_Transform;
  private Queue<PlatformSection> _platformSections_Pool_Disabled = new Queue<PlatformSection>();
  private Queue<PlatformSection> _platformSections_Pool_Enabled = new Queue<PlatformSection>();

  private WaitForSeconds _spawnWaitTime = new WaitForSeconds(0.05f);

  [EasyButtons.Button]
  public IEnumerator GeneratePool(int maxPoolCount)
  {
    for (int i = 0; i < maxPoolCount; i++)
    {
      PlatformSection secton = Instantiate(Platform_Prefab, SectionSpawn_Transform);
      secton.gameObject.SetActive(false);
      _platformSections_Pool_Disabled.Enqueue(secton);
      yield return _spawnWaitTime;
    }
  }

  // IEnumerator SpawnSection_Coro(int maxPoolCount)
  // {

  // }


  public PlatformSection GetItem()
  {
    PlatformSection section = (_platformSections_Pool_Disabled.Count > 0) ? _platformSections_Pool_Disabled.Dequeue() : Instantiate(Platform_Prefab, SectionSpawn_Transform);
    section.gameObject.SetActive(true);

    _platformSections_Pool_Enabled.Enqueue(section);
    return section;
  }


  public void BackToPool(PlatformSection item)
  {
    PlatformSection secton = _platformSections_Pool_Enabled.Dequeue();
    secton.OnSectionDestroy();
    secton.gameObject.SetActive(false);
    secton.transform.position = Vector3.down;
    _platformSections_Pool_Disabled.Enqueue(secton);
  }

  public (float, Vector3) OriginReset(float NextSpawnDistance)
  {
    float firstPos = 0;
    Vector3 pos = new Vector3(0, -1, -NextSpawnDistance);
    int index = 0;
    foreach (var item in _platformSections_Pool_Enabled)
    {
      if (index == 0) firstPos = item.transform.position.z;
      pos.z += NextSpawnDistance;
      item.transform.position = pos;
      index++;
    }
    return (firstPos, pos);
  }
}