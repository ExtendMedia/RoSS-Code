using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Scriptable Object class for projectiles
/// </summary>
[CreateAssetMenu(fileName = "New Projectile", menuName = "Data/Projectiles")]
public class ProjectileSO : SerializedScriptableObject
{
    [SerializeField] Projectile _prefab;
    public Projectile Prefab { get => _prefab; private set => _prefab = value; }

    [SerializeField] AudioClip[] _audioClips;
    public AudioClip[] AudioClips { get => _audioClips; private set => _audioClips = value; }
}
