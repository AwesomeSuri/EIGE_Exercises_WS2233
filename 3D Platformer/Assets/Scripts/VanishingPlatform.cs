using System.Collections;
using UnityEngine;

public class VanishingPlatform : MonoBehaviour
{
    [SerializeField] private float duration = 3;

    private Collider col;
    private Renderer rend;
    private bool isVisible;

    private void Start()
    {
        col = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
        isVisible = true;
        
        StartCoroutine(Vanishing());
    }

    private IEnumerator Vanishing()
    {
        while (true)
        {
            yield return new WaitForSeconds(duration);
            isVisible = !isVisible;
            col.enabled = rend.enabled = isVisible;
        }
    }
}
