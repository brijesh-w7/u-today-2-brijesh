using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public float interactRadius = 1.2f;

    public Transform handPoint;
    public Joystick joystick;

    private Rigidbody rb;
    private Animator animator;
    private IngredientObject heldIngredient;

    public IngredientObject HeldIngredient => heldIngredient;

    public Collider mCollider;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mCollider = GetComponent<Collider>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.IsPlaying)
        {
            rb.velocity = Constants.V3_Zero;
            return;
        }
        HandleMovement();
    }

    Vector3 dir;
    Vector3 scale;
    float h = 0;
    float v = 0;
    void HandleMovement()
    {
        // #if !UNITY_EDITOR
        h = joystick.Horizontal;
        v = joystick.Vertical;

        // #else
        // h = Input.GetAxisRaw("Horizontal");
        // v = Input.GetAxisRaw("Vertical");
        // #endif

        dir = new Vector3(v, 0, -h).normalized;
        rb.velocity = dir * moveSpeed;
    }


    void TryInteract(Collider collided)
    {
        if ((collided.tag == Constants.Station || collided.tag == Constants.Window) && collided.GetComponent<InteractableBase>() != null)
        {
            collided.GetComponent<InteractableBase>().Interact(this);
        }
    }

    public bool HasIngredient => heldIngredient != null;

    public void PickUp(IngredientObject ingredient)
    {
        heldIngredient = ingredient;
        ingredient.transform.SetParent(handPoint != null ? handPoint : transform);
        ingredient.transform.localPosition = Vector3.zero;
        SoundManager.Instance.Play(SoundManager.Instance.Clips.eatablePicked);
    }

    public IngredientObject DropHeld()
    {
        if (heldIngredient == null) return null;
        var ing = heldIngredient;
        heldIngredient = null;
        ing.transform.SetParent(null);
        SoundManager.Instance.Play(SoundManager.Instance.Clips.eatableDrop);

        return ing;

    }

    public void DiscardHeld()
    {
        if (heldIngredient == null) return;
        GameManager.Instance.ingredientSO.ReleaseToPool(heldIngredient);
        heldIngredient = null;
        SoundManager.Instance.Play(SoundManager.Instance.Clips.trashIn);
    }

    public void ResetPlayer()
    {
        if (heldIngredient != null)
        {
            GameManager.Instance.ingredientSO.ReleaseToPool(heldIngredient);
            heldIngredient = null;
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        TryInteract(other);
    }

}
