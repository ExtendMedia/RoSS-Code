using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Controls scene before battle
/// </summary>
public class BattlePrepareController : MonoBehaviour
{
    public event Action OnPrepareForBattleEnded;

    List<PrepareText> _prepareTexts = new List<PrepareText>();

    [SerializeField] TMP_Text _prepareTextUI;
    [SerializeField] GameObject _preparePanelUI;


    void Awake()
    {
        _prepareTexts.Add(new PrepareText("Prepare for battle", 2f));
        _prepareTexts.Add(new PrepareText("<size=150>3</size>", 1f));
        _prepareTexts.Add(new PrepareText("<size=150>2</size>", 1f));
        _prepareTexts.Add(new PrepareText("<size=150>1</size>", 1f));
        _prepareTexts.Add(new PrepareText("<size=180>GO!</size>", 1f));
    }

    IEnumerator DisplayText()
    {
        foreach (var prepareText in _prepareTexts)
        {
            _prepareTextUI.text = prepareText.Text;
            yield return new WaitForSeconds(prepareText.Duration);
        }
        OnPrepareForBattleEnded?.Invoke();
        _preparePanelUI.SetActive(false);

    }

    public void PrepareForBattle()
    {
        StartCoroutine(DisplayText());
    }
}

struct PrepareText
{
    public PrepareText(string text, float duration)
    {
        Text = text;
        Duration = duration;
    }
    public string Text;
    public float Duration;
}
