
using UnityEngine;


[CreateAssetMenu(fileName = "InputData", menuName = "ScriptableObjects/Input System")]
public class InputData : ScriptableObject
{
    public KeyCode interact = KeyCode.E;
    public KeyCode slash = KeyCode.J;
    public KeyCode jump = KeyCode.Space;
    public KeyCode dash = KeyCode.LeftShift;
    public string move = "Horizontal";
    public string look = "Vertical";
    public KeyCode pause = KeyCode.Escape;
}
