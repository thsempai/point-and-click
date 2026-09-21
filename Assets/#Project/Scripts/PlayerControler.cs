using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerControler : MonoBehaviour
{
    private const string ACTION_MAP = "Game";
    private const string CLICK_ACTION = "Click";
    private const string MOUSE_POSITION_ACTION = "Mouse Position";

    [SerializeField] private InputActionAsset inputActions;
    private InputAction click;
    private InputAction mousePosition;
    private NavMeshAgent agent;


    private void Awake()
    {
        click = inputActions.FindActionMap(ACTION_MAP).FindAction(CLICK_ACTION);
        mousePosition = inputActions.FindActionMap(ACTION_MAP).FindAction(MOUSE_POSITION_ACTION);

        click.performed += ctx => { OnClick(ctx); };
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnClick(InputAction.CallbackContext ctx)
    {
        Vector2 mousePos = mousePosition.ReadValue<Vector2>();
        Camera cam = Camera.main;

        Ray ray = cam.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GotoDestination(hit.point);
        }
    }

    private void GotoDestination(Vector3 position)
    {
        agent.SetDestination(position);
    }

    private void OnEnable()
    {
        inputActions.FindActionMap(ACTION_MAP).Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap(ACTION_MAP).Disable();
    }
}
