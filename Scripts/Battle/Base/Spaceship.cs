using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spaceship class
/// </summary>
public class Spaceship : MonoBehaviour
{
    public SpaceshipSO SpaceshipSO;
    [SerializeField] Transform _spaceshipObject;
    public List<Weapon> weaponList = new List<Weapon>();

    public void SetSpaceshipGO(Transform gameObject) => _spaceshipObject = gameObject;

    List<Vector3> GetFullDestructionsPoints()
    {
        List<Vector3> fullDestructionsPointList = new List<Vector3>();
        MeshRenderer meshRenderer = _spaceshipObject.GetComponent<MeshRenderer>();
        float offsetX = meshRenderer.bounds.size.x / SpaceshipSO.FullDestructionPoints;
        for (int i = 0; i < SpaceshipSO.FullDestructionPoints; i++)
        {
            fullDestructionsPointList.Add(new Vector3(meshRenderer.bounds.min.x + offsetX * i, Random.Range(meshRenderer.bounds.min.y * 0.25f, meshRenderer.bounds.max.y * 0.25f), meshRenderer.bounds.min.z));
        }
        return fullDestructionsPointList;
    }
    public void ActivateFullDestruction()
    {
        foreach (var point in GetFullDestructionsPoints())
        {
            var effect = BattleManager.Instance.GetEnemyPool().GetEffect(SpaceshipSO.FullDestructionEffect);
            effect.Init(point, Quaternion.identity);

        }
        Destroy(_spaceshipObject.gameObject);
    }

    public void SpawnEnemyShipWeapons(Transform parent)
    {
        foreach (var slot in SpaceshipSO.GetSlots())
        {
            var weaponItemSO = slot.Item as WeaponItemSO;
            var weaponGO = Instantiate<Transform>(weaponItemSO.Prefab, parent.transform);
            weaponGO.SetLocalPositionAndRotation(slot.Position, parent.transform.localRotation);
            weaponGO.gameObject.AddComponent<LookAtTarget>();
            var statsController = weaponGO.gameObject.AddComponent<StatsController>();
            statsController.Name = weaponItemSO.Name;
            statsController.SetHealth(weaponItemSO.Health);
            var weapon = weaponGO.gameObject.AddComponent<Weapon>();
            weapon.SetWeaponItemSO(weaponItemSO);
            weaponList.Add(weapon);
            statsController.OnDie += weapon.Destroy;
        }
    }
}
