using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public Transform firePoint;

    private Camera _mainCamera;

    [Header("Abilities")]
    public GameObject fireballAbilityObject;
    public GameObject meleeAbilityObject;

    private IAttackAbility _fireballAbility;
    private IAttackAbility _meleeAbility;

    void Start()
    {
        _mainCamera = Camera.main;

        _fireballAbility = fireballAbilityObject.GetComponent<IAttackAbility>();
       _fireballAbility = GetComponent<FireballAbility>();
        _meleeAbility = meleeAbilityObject.GetComponent<IAttackAbility>();
    }

    void Update()
    {
        Vector3 mousePos = _mainCamera.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            _mainCamera.nearClipPlane
        ));
        mousePos.z = 0f;

        Vector2 direction = (mousePos - firePoint.position).normalized;

        // Tecla E: bola de fuego
        if (Input.GetKeyDown(KeyCode.E))
        {
            _fireballAbility.UseAbility(firePoint, direction);
        }

        // LMB : ataque melee
        
        if (Input.GetMouseButtonDown(0))
        {
            _meleeAbility.UseAbility(firePoint, direction);
        }
    }
}