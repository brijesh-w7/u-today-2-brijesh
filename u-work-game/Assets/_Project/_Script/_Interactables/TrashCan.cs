public class TrashCan : InteractableBase
{
    public override void Interact(PlayerController player)
    {
        if (!GameManager.Instance.IsPlaying) return;

        if (!player.HasIngredient)
        {
            LogsManager.Log("Nothing to trash.", transform.position);
            return;
        }

        player.DiscardHeld();
        LogsManager.Log("Discarded!", transform.position);
    }
}
