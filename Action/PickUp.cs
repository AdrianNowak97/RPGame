using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PickUp : Action
{
    public ItemSO itemObject;

    GameObject cursor;
    GameObject player;
    private LookOnItemScript lookOnItemScript;

    private void Awake()
    {
        cursor = GameObject.FindWithTag("Cursor");
        player = GameObject.FindWithTag("Player");
        lookOnItemScript = FindObjectOfType<SecondView>().secondView;
    }

    protected override void OnAction()
    {
        if (lookOnItemScript.isLooking)
            return;

        TriggerStateChange("Grabing");

        if (Input.GetMouseButtonDown(0))
        {
            cursor.SetActive(false);
            player.GetComponent<FirstPersonController>().enabled = false;
            lookOnItemScript.ShowItem(itemObject, gameObject);
        }

        if (Input.GetMouseButtonDown(1))
        {
            lookOnItemScript.PickingUp(itemObject, gameObject);
        }
    }
}
