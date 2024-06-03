using System;
using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Controls the behavior of hit points after hit
/// </summary>
namespace RoSS
{
    public class HitPoints : MonoBehaviour
    {
        Camera _camera;
        [SerializeField] Vector3 _offset = new Vector3(0, -10f, 0);
        Vector3 _scale;

        [SerializeField] float _scaleOffset = 0.5f;

        [SerializeField] float _speed = 10f;
        [SerializeField] float _zPositionOffset = -1f;
        [SerializeField] TMP_Text _valueText;

        public float LifeTime = 2f;

        public event Action OnSpawn = delegate { };


        void Awake()
        {
            _camera = BattleManager.Instance.GetBatlleCamera();
            _scale = transform.localScale;
        }

        public void Spawn(Vector3 position, float value)
        {
            transform.position = new Vector3(position.x, position.y, position.z + _zPositionOffset);
            transform.localScale = _scale;
            _valueText.text = value.ToString();
            OnSpawn?.Invoke();
            StartCoroutine(MovingAndFading());
        }

        IEnumerator MovingAndFading()
        {
            Vector3 destination = transform.position + _offset;
            Vector3 targetScale = transform.localScale * _scaleOffset;
            while (transform.position != destination)
            {
                transform.position = Vector3.Slerp(transform.position, destination, Time.deltaTime * _speed);
                transform.localScale = Vector3.Slerp(transform.localScale, targetScale, Time.deltaTime * _speed);
                yield return null;
            }
        }
    }
}