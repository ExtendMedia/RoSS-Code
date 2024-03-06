using UnityEngine;

/// <summary>
/// Rotates the gameobject
/// </summary>
public class Rotator : MonoBehaviour
{
    [SerializeField] Vector3 _rotation;
    [SerializeField] float _speed;

    void Update()
    {
        transform.Rotate(_rotation * _speed * Time.deltaTime);
    }
}
