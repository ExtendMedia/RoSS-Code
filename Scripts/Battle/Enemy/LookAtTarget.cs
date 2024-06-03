using UnityEngine;

/// <summary>
/// Controls turrets rotation so that they look at the target (i.e. player) 
/// </summary>
namespace RoSS
{
    public class LookAtTarget : MonoBehaviour
    {
        Transform _target;
        [SerializeField] Transform _barrel;
        [SerializeField] float _rotationSpeed = 1;


        void Start()
        {
            _target = BattleManager.Instance.GetTarget();
        }

        void Update()
        {
            RotateTurretBase();

            if (_barrel != null)
                TiltTurretBarrels();

        }


        void RotateTurretBase()
        {
            Vector3 baseDirection = Vector3.ProjectOnPlane(_target.position - transform.position, -transform.up).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(baseDirection, -Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }

        void TiltTurretBarrels()
        {
            Vector3 tiltDirection = Vector3.ProjectOnPlane(_target.position - _barrel.transform.position, -_barrel.transform.right).normalized;
            Quaternion tiltRotation = Quaternion.LookRotation(tiltDirection, transform.up);
            _barrel.transform.rotation = Quaternion.Slerp(_barrel.transform.rotation, tiltRotation, Time.deltaTime * _rotationSpeed);
        }


    }
}