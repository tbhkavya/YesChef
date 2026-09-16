using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform holdPoint;

    private GameObject heldIngredient;

    public bool IsHolding => heldIngredient != null;

    public bool TryHold(GameObject ingredient)
    {
        if (IsHolding)
            return false;

        heldIngredient = ingredient;

        ingredient.transform.SetParent(holdPoint);
        ingredient.transform.localPosition = Vector3.zero;
        ingredient.transform.localRotation = Quaternion.identity;

        return true;
    }

    public GameObject GetHeldIngredient()
    {
        return heldIngredient;
    }

    public void ClearHand()
    {
        heldIngredient = null;
    }
}