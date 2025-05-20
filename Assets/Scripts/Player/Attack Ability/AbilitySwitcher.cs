using UnityEngine;

public class AbilitySwitcher : MonoBehaviour
{
    public AbilityController abilityController;

    public GameObject fireballAbilityObject;
    public GameObject meleeAbilityObject;

    private IAttackAbility _fireballAbility;
    private IAttackAbility _meleeAbility;
    private bool _isUsingFireball = true;

    private void Start()
    {
        _fireballAbility = fireballAbilityObject.GetComponent<IAttackAbility>();
        _meleeAbility = meleeAbilityObject.GetComponent<IAttackAbility>();

        abilityController.SetAbility(_fireballAbility);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _isUsingFireball = !_isUsingFireball;

            if (_isUsingFireball)
            {
                abilityController.SetAbility(_fireballAbility);
                Debug.Log("Switched to Fireball");
            }
            else
            {
                abilityController.SetAbility(_meleeAbility);
                Debug.Log("Switched to Melee");
            }
        }
    }
}