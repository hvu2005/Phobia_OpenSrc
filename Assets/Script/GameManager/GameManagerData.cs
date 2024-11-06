
using UnityEngine;

[CreateAssetMenu(fileName = "GameManagerData", menuName = "ScriptableObjects/GameManagerData", order = 1)]
public class GameManagerData : ScriptableObject
{
    public GameObject canvas;
    public GameObject startCrossFade;
    public GameObject endCrossFade;
    public string[] sceneName;
}
