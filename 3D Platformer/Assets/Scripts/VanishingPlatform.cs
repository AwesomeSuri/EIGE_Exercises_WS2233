using System;
using System.Collections;
using UnityEngine;

public class VanishingPlatform : MonoBehaviour
{
    [SerializeField] private float duration = 3;

    private BoxCollider col;
    private Renderer rend;
    private bool isVisible;

    private void Start()
    {
        col = GetComponent<BoxCollider>();
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

    private void OnDrawGizmos()
    {
        if (col == null) col = GetComponent<BoxCollider>();
        
        Gizmos.color = Color.red;
        var rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(col.center, col.size);
    }
}
