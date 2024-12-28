using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput = null;

    public void Init()
    {
        if(playerInput == null)
        {
            playerInput = this.AddComponent<PlayerInput>();
            playerInput.actions = Managers.Resource.Load<InputActionAsset>("Input/InputActions");
        }
    }

    public void OnMove()
    {
        Debug.Log("dd");
    }
}
