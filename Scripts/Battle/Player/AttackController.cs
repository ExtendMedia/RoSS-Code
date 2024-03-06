using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls player's attacks
/// </summary>
public class AttackController : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] GameObject _iconFocus;
    [SerializeField] ItemType _weaponType;
    [SerializeField] AttackWeapon _attackWeaponPrefab;


    Transform _spawner;
    Transform _target;
    //List<WeaponItemSO> _weapons = new List<WeaponItemSO>();
    List<AttackWeapon> _weaponUI = new List<AttackWeapon>();

    int _activeWeaponCount = 0;
    void Awake()
    {
        _button.onClick.AddListener(() => Shoot());
        //_weapons = GameManager.Instance.Player.ActiveSpaceship.GetWeapons(_weaponTypeSO);
    }
    void Start()
    {
        _spawner = BattleManager.Instance.GetSpawner();
        _target = BattleManager.Instance.GetTarget();
        SetButtons();
    }

    public void Shoot()
    {
        if (BattleManager.Instance.BattleState != BattleState.Battle) return;

        if (_activeWeaponCount <= 0) return;
        foreach (var weapon in _weaponUI)
        {
            if (!weapon.IsReloading())
            {
                weapon.StartReloading();
                _activeWeaponCount--;
                UpdateButtonIcon();
                weapon.SpawnProjectile(_target);
                break;
            }
        }
    }




    void SetButtons()
    {
        float rotation = 0;
        int i = 1;
        ProjectileSpawner[] spawners = _spawner.GetComponentsInChildren<ProjectileSpawner>();
        int spawnerIndex = 0;

        foreach (var slotsGroup in GameManager.Instance.Player.ActiveSpaceship.GetWeaponsInSlots(_weaponType))
        {
            foreach (WeaponItemSO weapon in slotsGroup.Value)
            {

                AttackWeapon attackWeapon;
                if (_attackWeaponPrefab != null)
                {
                    attackWeapon = Instantiate<AttackWeapon>(_attackWeaponPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, rotation)), transform);

                }
                else
                {
                    GameObject attackWeaponGO = new GameObject("Attack " + i++);
                    attackWeaponGO.transform.SetPositionAndRotation(transform.position, transform.rotation);
                    attackWeaponGO.transform.SetParent(transform);
                    attackWeapon = attackWeaponGO.AddComponent<AttackWeapon>();
                }
                rotation -= 90f;

                attackWeapon.SetWeapon(weapon);
                if (spawners.Length > slotsGroup.Key && spawners[slotsGroup.Key] != null) spawnerIndex = slotsGroup.Key;
                attackWeapon.SetSpawner(spawners[spawnerIndex].transform);
                attackWeapon.OnWeaponReloaded += WeaponReloaded;
                _weaponUI.Add(attackWeapon);
            }
        }
    }

    void WeaponReloaded()
    {
        _activeWeaponCount++;
        UpdateButtonIcon();
    }

    void UpdateButtonIcon()
    {
        if (_iconFocus) _iconFocus.SetActive(_activeWeaponCount > 0);
    }
}
