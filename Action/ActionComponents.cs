using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(BoxCollider))]
public class ActionComponents : MonoBehaviour
{
    private float defoultColiderRadius = 6;
    private Vector3 defoultColiderSize = new Vector3(2, 2, 2);

    private void Reset() //ustawia przykładowe wartości podczas tworzenia skryptu
    {
        GetComponent<SphereCollider>().isTrigger = true;
        GetComponent<SphereCollider>().radius = defoultColiderRadius;
        GetComponent<BoxCollider>().isTrigger = true;
        GetComponent<BoxCollider>().size = defoultColiderSize;
    }
}
