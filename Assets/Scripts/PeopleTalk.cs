using NodeCanvas.DialogueTrees;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleTalk : MonoBehaviour
{
    private DialogueTreeController dialogueTree;

    private void Start()
    {
        dialogueTree = GetComponent<DialogueTreeController>();
    }
    public void Talk()
    {
        dialogueTree.StartDialogue();
    }

    public void StopDia() {
        dialogueTree.StopDialogue();
        
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
