using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Scriptable Object class for the audiovisual effect
/// </summary>
[CreateAssetMenu(fileName = "New Effect", menuName = "Data/Effects")]
public class EffectSO : SerializedScriptableObject
{
    [SerializeField] Effect _prefab;
    public Effect Prefab { get => _prefab; private set => _prefab = value; }

    [SerializeField] AudioClip[] _audioClips;
    public AudioClip[] AudioClips { get => _audioClips; private set => _audioClips = value; }
}
