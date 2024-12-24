using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }

    [SerializeField] private PlayerInput data;

    public PlayerInput Data => data;

    public bool canGetAction { get; set; } = true;
    public float move { get; private set; }
    public float look { get; private set; }
    public bool isJumping { get; private set; }
    public bool isSlashing { get; private set; }
    public bool slash {  get; private set; }
    public bool isGetAnyKeyDown { get; private set; }

    public bool pause { get; private set; }

    private bool _canSlash = true;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
        isGetAnyKeyDown = Input.anyKeyDown;
        if (!canGetAction) return;
        Moving();
        Jumping();
        Slashing();
    }
    private void Pause()
    {
        pause = data.actions["pause"].WasPressedThisFrame();
    }
    private void Moving()
    {
        move = data.actions["move"].ReadValue<float>();
        look = data.actions["look"].ReadValue<float>();
    }
    private void Jumping()
    {
        isJumping = data.actions["jump"].IsPressed();
    }
    #region Slashing
    private void Slashing()
    {
        slash = _canSlash && data.actions["slash"].WasPressedThisFrame();
        if(slash)
        {
            StartCoroutine(IESlashing());
        }
    }
    private IEnumerator IESlashing()
    {
        _canSlash = false;
        isSlashing = true;
        yield return new WaitForSeconds(0.25f);
        isSlashing = false;
        yield return new WaitForSeconds(0.1f);
        _canSlash = true;
    }
    #endregion
}
