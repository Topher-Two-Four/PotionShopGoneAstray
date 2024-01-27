using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkTestScript : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story story;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSON.text);
        Debug.Log(LoadStoryChunk());

        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            Debug.Log(story.currentChoices[i].text);
        }

        story.ChooseChoiceIndex(0);

        Debug.Log(LoadStoryChunk());

        Debug.Log(LoadStoryChunk());
    }

    // Update is called once per frame
    void Update()
    {
        
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
