using TMPro;
using UnityEngine;

/// <summary>
/// Controls the turret's healthbar
/// </summary>
namespace RoSS
{
    public class TurretHealthBar : HealthBar
    {

        Camera _camera;
        float _positionOffsetY;
        float _positionOffsetX;
        [SerializeField] TMP_Text _weaponNameText;

        protected override void Awake()
        {
            _camera = BattleManager.Instance.GetBatlleCamera();
            base.Awake();
        }

        public override void InitHealthBar(StatsController statsController)
        {
            base.InitHealthBar(statsController);
            SetName(statsController.Name);
            SetPosition();
        }

        void SetPosition()
        {
            _positionOffsetY = _statsController.GetComponent<MeshRenderer>().bounds.size.x / 2;
            _positionOffsetX = _statsController.GetComponent<MeshRenderer>().bounds.size.z / 2;
        }

        void SetName(string name) => _weaponNameText.text = name;


        void LateUpdate()
        {
            transform.position = _camera.WorldToScreenPoint(_statsController.transform.position + new Vector3(_positionOffsetX, _positionOffsetY, 0));
        }


    }
}