
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    public static State curentState;
    [SerializeField] Image cursor;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        Action.ChangeState += ChangeState;
        DialogueCanvasManager.ChangeState += ChangeState;
    }

    public void ChangeState(string stateName)
    {
        State state = StateFactor.inst.GetState(stateName);
        SetCursorIcon(state.Sprite);
        curentState = state;
    }

    public void SetCursorIcon(Sprite icon)
    {
        if (DialogueCanvasManager.inst.isDialogueCanvasOpen)
        {
            cursor.sprite = null;
            cursor.color = new Color32(0, 0, 0, 0);
        }
        else
        {
            cursor.sprite = icon;
            cursor.color = new Color32(255, 255, 255, 255);
        }
    }
}