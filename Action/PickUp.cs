using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PickUp : Action
{
    public ItemSO itemObject;

    GameObject coursor;
    GameObject player;
    private LookOnItemScript lookOnItemScript;

    private void Awake()
    {
        coursor = GameObject.FindWithTag("Coursor");
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
            coursor.SetActive(false);
            player.GetComponent<FirstPersonController>().enabled = false;
            lookOnItemScript.ShowItem(itemObject, gameObject);
        }

        if (Input.GetMouseButtonDown(1))
        {
            lookOnItemScript.PickingUp(itemObject, gameObject);
        }
    }
}
