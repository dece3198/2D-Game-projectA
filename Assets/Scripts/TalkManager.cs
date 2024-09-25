using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    public static TalkManager instance;
    [SerializeField] private GameObject talk;
    [SerializeField] private Image charactorImage;
    [SerializeField] private TextMeshProUGUI talkText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private NPC[] npcs;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeTalk(NPC npc ,Talk _talk)
    {
        if(_talk.talkCount < _talk.talks.Length)
        {
            talk.SetActive(true);
            charactorImage.sprite = _talk.talks[_talk.talkCount].NpcImage;
            nameText.text = _talk.talks[_talk.talkCount].name;
            StartCoroutine(TalkCo(_talk));
        }   
        else
        {
            talk.SetActive(false);

            if(_talk.nextTalk != null)
            {
                npc.talk = _talk.nextTalk;
            }
        }
    }

    public void NextDay()
    {
        for(int i = 0; i < npcs.Length; i++)
        {
            if (npcs[i].talk.nextDay != null)
            {
                if (npcs[i].talk.talkCount > 0)
                {
                    npcs[i].talk = npcs[i].talk.nextDay;
                }
            }
        }
    }

    private IEnumerator TalkCo(Talk _talk)
    {
        for(int i = 0; i <= _talk.talks[_talk.talkCount].conversation.Length; i++)
        {
            talkText.text = _talk.talks[_talk.talkCount].conversation.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }

        _talk.talkCount++;
    }
}
