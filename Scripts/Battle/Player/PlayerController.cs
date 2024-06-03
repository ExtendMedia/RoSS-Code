using UnityEngine;

/// <summary>
/// Main player controller. Controlls movement and shooting
/// </summary>
namespace RoSS
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInputActions _playerInput;

        private CharacterController _controller;
        [SerializeField]
        private float playerSpeed = 10.0f;

        [SerializeField] AttackController _mainAttack;
        [SerializeField] AttackController _specialAttack;


        private void Awake()
        {
            _playerInput = new PlayerInputActions();
            _controller = GetComponent<CharacterController>();

        }
        private void OnEnable()
        {
            _playerInput.Enable();
        }
        private void OnDisable()
        {
            _playerInput.Disable();
        }
        void Update()
        {
            Vector2 movementInput = _playerInput.PlayerMain.Move.ReadValue<Vector2>();
            Vector3 move = new Vector3(movementInput.x, movementInput.y, 0f);
            _controller.Move(move * Time.deltaTime * playerSpeed);

            if (_playerInput.PlayerMain.MainAttack.triggered)
            {
                _mainAttack.Shoot();

            }
            else if (_playerInput.PlayerMain.SpecialAttack.triggered)
            {
                _specialAttack.Shoot();
            }

        }

    }
}