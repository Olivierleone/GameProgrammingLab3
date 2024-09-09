using UnityEngine;

public class TargetComponent : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with an object tagged "Projectile"
        if (collision.gameObject.CompareTag("Projectile"))
        {
            // Increment score via the GameManager
            GameManager.Instance.IncrementScore();

            // Change the target color to green
            Renderer parentRenderer = transform.parent.GetComponent<Renderer>();
            if (parentRenderer != null)
            {
                parentRenderer.material.color = Color.green; // Change color to green
                Invoke("ChangeColorBack", 5f);  // Call ChangeColorBack after 5 seconds
            }

            // Hide the target after 5 seconds
            Invoke("HideTarget", 5f);
        }
    }

    // Method to hide the target after a delay
    void HideTarget()
    {
        gameObject.SetActive(false);
    }

    // Method to change the color back to red
    void ChangeColorBack()
    {
        Renderer parentRenderer = transform.parent.GetComponent<Renderer>();
        if (parentRenderer != null)
        {
            parentRenderer.material.color = Color.red; // Change color back to red
        }
    }
}
