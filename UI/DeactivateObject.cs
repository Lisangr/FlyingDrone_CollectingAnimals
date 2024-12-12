using UnityEngine;

public class DeactivateObject : MonoBehaviour
{
    public GameObject objectToDeactivate;
    public void OnClickDeactivateObject()
        { objectToDeactivate.SetActive(false); }
}
