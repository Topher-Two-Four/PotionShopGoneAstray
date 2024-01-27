using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;

public class InkTestScript : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story story;

    public TMP_Text textPrefab;
    public Button buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSON.text);

        RefreshUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RefreshUI()
    {
        EraseUI();

        TMP_Text storyText = Instantiate(textPrefab) as TMP_Text;
        storyText.text = LoadStoryChunk();
        storyText.transform.SetParent(this.transform, false);

        foreach (Choice choice in story.currentChoices)
        {
            Button choiceButton = Instantiate(buttonPrefab) as Button;
            choiceButton.transform.SetParent(this.transform, false);

            TMP_Text choiceText = buttonPrefab.GetComponentInChildren<TMP_Text>();
            choiceText.text = choice.text;

            choiceButton.onClick.AddListener(delegate { ChooseStoryChoice(choice); });
        }
    }

    void EraseUI()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    void ChooseStoryChoice(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshUI();
    }

    string LoadStoryChunk()
    {
        string text = "";

        if(story.canContinue)
        {
            text = story.ContinueMaximally();
        }

        return text;
    }

}
