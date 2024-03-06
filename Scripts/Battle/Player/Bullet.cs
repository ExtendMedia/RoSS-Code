using System;
using UnityEngine;

/*
[Obsolete]
public class Bullet : MonoBehaviour
{
    public readonly float speed = 10f;
    public readonly float damage = 5f;

    public void ResetPositionAndRotation(Transform shootPoint)
    {
        gameObject.transform.SetPositionAndRotation(shootPoint.position, shootPoint.rotation);

    }
    public void Hit(GameObject target)
    {
        var targetStatsController = target.GetComponent<StatsController>();
        if (targetStatsController == null)
        {
            targetStatsController = target.GetComponentInParent<StatsController>();
        }
        if (targetStatsController != null)
        {
            targetStatsController.ChangeHealth(-damage);
        }
         
    }
}
*/