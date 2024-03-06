using UnityEngine;

/// <summary>
/// Controls turrets rotation so that they look at the target (i.e. player) 
/// </summary>
public class LookAtTarget : MonoBehaviour
{
    Transform _target;
    [SerializeField] float _rotationSpeed = 1;
    void Start()
    {
        _target = BattleManager.Instance.GetTarget();
    }

    void Update()
    {
        Vector3 direction = (new Vector3(_target.position.x, _target.position.y, transform.position.z) - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction, -Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
    }

}
