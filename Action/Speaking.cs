using System;
using UnityEngine;

public class Speaking : Action
{
    public DialogueSO dialogueSO;
    [SerializeField] private bool automaticTrigger = false;

    private DialogueCanvasManager dialogueCanvasManager;

    private void Start()
    {
        dialogueCanvasManager = DialogueCanvasManager.inst;
    }

    protected override void OnAction()
    {

        if (!(DialogueCanvasManager.inst.CheckAnyPanelFulfil(dialogueSO)))
            return;

        if (DialogueCanvasManager.inst.CheckIfAnyNotNarratorDialog(dialogueSO))
        {
            TriggerStateChange("Dialog");
        }
        else
        {
            TriggerStateChange("Narrator");
        }

        //Zamknij dialog  gdy escape
        if (Input.GetKeyDown(KeyCode.Escape) && dialogueCanvasManager.isDialogueCanvasOpen)
        {
            dialogueCanvasManager.CloseDialogueCanvas();
        }
        else if (automaticTrigger && dialogueCanvasManager.isDialogueCanvasOpen == false)
        {
            dialogueCanvasManager.OpenDialogueCancas(dialogueSO);
            gameObject.SetActive(false);
        }
        else if (dialogueCanvasManager.isDialogueCanvasOpen == false && Input.GetMouseButtonDown(0))
        {
            dialogueCanvasManager.OpenDialogueCancas(dialogueSO);
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        if (dialogueCanvasManager.isDialogueCanvasOpen)
        {
            dialogueCanvasManager.CloseDialogueCanvas();
        }
    }

    
}
