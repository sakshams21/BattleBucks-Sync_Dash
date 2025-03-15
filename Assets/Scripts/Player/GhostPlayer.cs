using DG.Tweening;
using UnityEngine;

public class GhostPlayer : MonoBehaviour
{
    [SerializeField] private GameObject ParticleEffect_Ghost;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable_Ghost"))
        {
            other.gameObject.SetActive(false);
            ParticleEffect_Ghost.SetActive(false);
            ParticleEffect_Ghost.SetActive(true);
        }

        if (other.CompareTag("Obstacle_Ghost"))
        {
            other.enabled = false;
            other.GetComponent<MeshRenderer>().materials[0].DOFloat(1, "_Dissolve", 0.5f).
            OnComplete(() =>
            {
                other.gameObject.SetActive(false);
            });
        }
    }
}
