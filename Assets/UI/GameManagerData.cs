
using UnityEngine;

[CreateAssetMenu(fileName = "GameManagerData", menuName = "ScriptableObjects/GameManagerData", order = 1)]
public class GameManagerData : ScriptableObject
{
    public GameObject StartCrossFade;
    public GameObject EndCrossFade;
    public string[] sceneName;
}
