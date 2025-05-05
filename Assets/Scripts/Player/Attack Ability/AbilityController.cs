using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public Transform firePoint;
    private IAttackAbility _currentAbility;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        _currentAbility = GetComponent<IAttackAbility>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = _mainCamera.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mainCamera.nearClipPlane)
            );
            mousePos.z = 0f;

            Vector2 direction = (mousePos - firePoint.position).normalized;
            _currentAbility.UseAbility(firePoint, direction);
        }
    }

    public void SetAbility(IAttackAbility newAbility)
    {
        _currentAbility = newAbility;
    }
}