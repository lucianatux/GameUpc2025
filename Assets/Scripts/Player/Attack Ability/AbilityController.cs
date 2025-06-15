using UnityEngine;

public class AbilityController : MonoBehaviour // This class handles the player's abilities, such as melee and ranged (e.g., fireball) attacks.
{
    public Transform firePoint;

    private Camera _mainCamera;

    [Header("Abilities")]
    public GameObject fireballAbilityObject;
    public GameObject meleeAbilityObject;

    private IAttackAbility _fireballAbility;
    private IAttackAbility _meleeAbility;

    private PlayerAnimatorController animatorController;

    private PlayerMovement _playerMovement;
    void Start()
    {
        _mainCamera = Camera.main;
        _playerMovement = GetComponent<PlayerMovement>();

        if (_playerMovement == null)
        {
            Debug.LogError("player movement not found");
        }

        _fireballAbility = fireballAbilityObject.GetComponent<IAttackAbility>();

        if (fireballAbilityObject == null)
        {
            Debug.LogError("Fireball ability prefan not found");
        }

        _fireballAbility = GetComponent<FireballAbility>();

        if (_fireballAbility == null)
        {
            Debug.LogError("fireball ability in fireball prefab not found");
        }

        //_meleeAbility = meleeAbilityObject.GetComponent<IAttackAbility>();

        _meleeAbility = GetComponent<MeleeAbility>();
        if (_meleeAbility == null)
        {
            Debug.LogError("Melee ability not found");
        }

    }

    void Awake()
    {
        animatorController = GetComponent<PlayerAnimatorController>();
    }

    void Update()
    {
        Vector3 mousePos = _mainCamera.ScreenToWorldPoint(new Vector3( // Convert mouse position from screen space to world space.
        Input.mousePosition.x,
        Input.mousePosition.y,
        _mainCamera.nearClipPlane
        ));
        
        mousePos.z = 0f;

        Vector2 direction = (mousePos - firePoint.position).normalized; // Calculate the direction from the firePoint to the mouse position.
        
        // RMB: ataque melee
        if (Input.GetMouseButtonDown(0))
        {
            _meleeAbility.UseAbility(firePoint, direction);

            StartCoroutine(_playerMovement.StunPlayer(.5f));

            animatorController.TriggerAnim("kick");
            PlayerEventsManager.Instance.PlayerKick();
        }
        // LMB: bola de fuego
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(_playerMovement.StunPlayer(.2f));

            _fireballAbility.UseAbility(firePoint, direction);
            animatorController.TriggerAnim("fireball");
            PlayerEventsManager.Instance.PlayerFireball();
        }
    }
}