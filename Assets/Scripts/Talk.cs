using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Talks
{
    public Sprite NpcImage;
    public string name;
    [SerializeField, TextArea(2, 5)]
    public string conversation;
}


[CreateAssetMenu(fileName = "New Talk", menuName = "New Talk/ Talk")]
public class Talk : ScriptableObject
{
    public Talks[] talks;
    public int talkCount;
    public Talk nextTalk;
}
