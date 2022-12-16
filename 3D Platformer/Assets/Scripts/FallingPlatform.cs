using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private string triggerTag = "Player";
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(triggerTag))
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
