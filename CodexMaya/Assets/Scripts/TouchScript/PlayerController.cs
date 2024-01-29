using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public bool tapped = false;
    // Start is called before the first frame update
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    public void OnTap(InputAction.CallbackContext context)
    {
        tapped = context.action.triggered;
    }

    // Update is called once per frame
   void Update()
    {
      if (tapped == true)
        {
            Debug.Log("tapped");
        }  
    }
}
