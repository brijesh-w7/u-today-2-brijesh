using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractableBase : MonoParent
{
    public abstract void Interact(PlayerController player);

    public SpriteRenderer highlightRenderer;

    protected virtual void OnTriggerEnter(Collider other)
    {
        // LogsManager.Log(gameObject.name, " InteractableBase: ");
        if (other.CompareTag("Player") && highlightRenderer != null)
            highlightRenderer.enabled = true;
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        // LogsManager.Log(gameObject.name, " InteractableBase: ");
        if (other.CompareTag("Player") && highlightRenderer != null)
            highlightRenderer.enabled = false;
    }

    protected bool IsTouchingPlayer(Collider currentCollider)
    {
        int mask = LayerMask.GetMask("Player");
        Collider playerCollider = GameManager.Instance.playerController.mCollider;
        Collider[] colliders = Physics.OverlapBox(currentCollider.bounds.center, currentCollider.bounds.extents, Quaternion.identity, layerMask: mask);
        foreach (var col in colliders)
        {
            if (col == playerCollider)
            {
                LogsManager.Log(MethodName, col.name, "YES");
                return true;
            }
            LogsManager.Log(MethodName, col.name);
        }

        return false;

    }
}
