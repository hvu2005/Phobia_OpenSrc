using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [SerializeField] private InputData _data;
    public bool canGetAction { get; set; } = true;
    public float move { get; private set; }
    public float look { get; private set; }
    public bool isJumping { get; private set; }
    public bool isSlashing { get; private set; }
    public bool isGetAnyKeyDown {  get; private set; } 
    // Start is called before the first frame update
    void Start()
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
        isGetAnyKeyDown = Input.anyKeyDown;
        if (!canGetAction) return;
        Moving();
        Jumping();
        Slashing();
    }
    private void Moving()
    {
        move = Input.GetAxis(_data.move);
        look = Input.GetAxisRaw(_data.look);
    }
    private void Jumping()
    {
        isJumping = Input.GetKey(_data.jump);
    }
    private void Slashing()
    {
        isSlashing = Input.GetKey(_data.slash);
    }
}
