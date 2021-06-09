using System;
using System.CodeDom;
using System.Linq;
using System.Reflection;
using UnityEngine;

public abstract class Action : ActionComponents
{
    public static event Action<string> ChangeState;
    private Ray ray;
    protected RaycastHit hit;

    protected abstract void OnAction();

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player")
        {
            TriggerStateChange("Idle");
            return;
        }

        if (!Camera.main || !Camera.main.isActiveAndEnabled)
        {
            TriggerStateChange("Idle");
            return;
        }

        ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        if (Physics.Raycast(ray, out hit, 15))
        {
            if (hit.collider.GetType() != typeof(BoxCollider))
            {
                TriggerStateChange("Idle");
                return;
            }

            if (hit.collider.gameObject.GetComponent(typeof(Action)) != this)
                return;

            OnAction();
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        TriggerStateChange("Idle");
    }

    protected void TriggerStateChange(string stateName)
    {
        State state = StateFactor.inst.GetState(stateName);
        if (ChangeState != null && StateManager.curentState != state)
        {
            ChangeState(stateName);
        }
    }

    private void OnDestroy()
    {
        TriggerStateChange("Idle");
    }
}
