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

    private void Awake()
    {
        instance = this;
    }

    public void ChangeTalk(Talk _talk)
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
