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

    void Start()
    {
        _mainCamera = Camera.main;

       _fireballAbility = fireballAbilityObject.GetComponent<IAttackAbility>();
       _fireballAbility = GetComponent<FireballAbility>();
        _meleeAbility = meleeAbilityObject.GetComponent<IAttackAbility>();
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
        
        // RMB o tecla E: ataque melee
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {
            _meleeAbility.UseAbility(firePoint, direction);
            animatorController.TriggerAnim("kick");
            PlayerEventsManager.Instance.PlayerKick();
        }
        // LMB: bola de fuego
        if (Input.GetMouseButtonDown(1))
        {
            _fireballAbility.UseAbility(firePoint, direction);
            animatorController.TriggerAnim("fireball");
            PlayerEventsManager.Instance.PlayerFireball();
        }
    }
}