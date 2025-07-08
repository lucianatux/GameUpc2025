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

    public float cooldownDurationFireBall = 0.5f;
    private float cooldownTimerFireBall = 0f;
    public bool IsReadyFireBall => cooldownTimerFireBall <= 0f;
    private bool _isUnlockedFireBall = false;

    public float cooldownDurationKick = 1f;
    private float cooldownTimerKick = 0f;
    public bool IsReadyKick => cooldownTimerKick <= 0f;

    private Vector2 direction;

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
            Debug.LogError("Fireball ability prefab not found");
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

    //gets called on animator
    public void ResetCooldown()
    {
        cooldownTimerKick = 0;
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


        if (cooldownTimerFireBall > 0f)
            cooldownTimerFireBall -= Time.deltaTime;

        if (cooldownTimerKick > 0f)
            cooldownTimerKick -= Time.deltaTime;


        mousePos.z = 0f;

        direction = (mousePos - firePoint.position).normalized; // Calculate the direction from the firePoint to the mouse position.

        // RMB: ataque melee
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsReadyKick) return; // no disparar si está en cooldown

            cooldownTimerKick = cooldownDurationKick;


            _meleeAbility.UseAbility(firePoint, direction);

            StartCoroutine(_playerMovement.StunPlayer(.1f));

            animatorController.TriggerAnim("kick");

            PlayerEventsManager.Instance.PlayerKick();
        }
        // LMB: bola de fuego
        if (Input.GetMouseButtonDown(1))
        {
            ShootFireBall();
        }
    }

    private void ShootFireBall()
    {
        if (!IsReadyFireBall || !_isUnlockedFireBall) return; // no disparar si está en cooldown

        cooldownTimerFireBall = cooldownDurationFireBall;

        StartCoroutine(_playerMovement.StunPlayer(.1f));

        _fireballAbility.UseAbility(firePoint, direction);
        animatorController.TriggerAnim("fireball");
        PlayerEventsManager.Instance.PlayerFireball();
    }

    //Gets called in Aji script
    public void UnlockFireBall()
    {
        if (_isUnlockedFireBall == true) return;
        Debug.Log("Unlocks FireBall");
        _isUnlockedFireBall = true;
    }


}