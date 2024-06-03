using System.Collections;
using UnityEngine;

/// <summary>
/// Smooth moves the gameobject to a specific position
/// </summary>
namespace RoSS
{
    public class SmoothMove : MonoBehaviour
    {
        [SerializeField] Vector3 _offset;
        [SerializeField] float _speed = 10f;

        public void StartMove()
        {
            StartCoroutine(Moving());
        }

        public void SetOffset(Vector3 offset) => _offset = offset;

        IEnumerator Moving()
        {
            Vector3 destination = transform.position + _offset;
            while (transform.position != destination)
            {
                transform.position = Vector3.Slerp(transform.position, destination, Time.deltaTime * _speed);
                yield return null;
            }
        }

    }
}