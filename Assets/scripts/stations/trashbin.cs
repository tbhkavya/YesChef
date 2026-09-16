using UnityEngine;

public class TrashBin : MonoBehaviour, IInteractable
{
    [Header("Player")]
    [SerializeField] private PlayerInventory playerInventory;

    [Header("Ingredient Pools")]
    [SerializeField] private ObjectPool vegetablePool;
    [SerializeField] private ObjectPool cheesePool;
    [SerializeField] private ObjectPool meatPool;
    [SerializeField] private GameManager gameManager;

    public void Interact()
    {
        if (!gameManager.IsGameRunning)
        
        if (!playerInventory.IsHolding)
        {
            Debug.Log("Your hands are empty.");
            return;
        }

        GameObject ingredient =
            playerInventory.GetHeldIngredient();

        Ingredient ingredientData =
            ingredient.GetComponent<Ingredient>();

        if (ingredientData == null)
        {
            Debug.Log("Invalid ingredient.");
            return;
        }

    
        playerInventory.ClearHand();

        
        switch (ingredientData.Type)
        {
            case IngredientType.Vegetable:
                vegetablePool.Return(ingredient);
                break;

            case IngredientType.Cheese:
                cheesePool.Return(ingredient);
                break;

            case IngredientType.Meat:
                meatPool.Return(ingredient);
                break;
        }

        Debug.Log("Ingredient thrown in trash.");
    }
}