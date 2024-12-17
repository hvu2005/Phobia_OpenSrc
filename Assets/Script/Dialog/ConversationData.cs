using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Conversation Name", menuName = "ScriptableObjects/NPC Conversation")]
public class ConversationData : ScriptableObject
{
    public Sprite avatar;
    public AvatarSide avtSide;
    public string speakerName;

    [TextArea(3,10)]
    public string[] paragraphs;
}

public enum AvatarSide
{
    Left, 
    Right 
};
