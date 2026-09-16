using UnityEngine;

public class Refrigerator : MonoBehaviour, IInteractable
{
    [Header("Ingredient Pools")]
    [SerializeField] private ObjectPool vegetablePool;
    [SerializeField] private ObjectPool cheesePool;
    [SerializeField] private ObjectPool meatPool;

    [SerializeField] private GameManager gameManager;

    [Header("Player")]
    [SerializeField] private PlayerInventory playerInventory;

    private bool isSelecting;

    public void Interact()
    {
        if (!gameManager.IsGameRunning)
            return;

        if (playerInventory.IsHolding)
        {
            Debug.Log("Hands are full!");
            return;
        }

        isSelecting = true;

        Debug.Log("Choose ingredient: 1 = Vegetable, 2 = Cheese, 3 = Meat");
    }

    private void Update()
    {
        if (!isSelecting)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GiveIngredient(vegetablePool);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GiveIngredient(cheesePool);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GiveIngredient(meatPool);
        }
    }

    private void GiveIngredient(ObjectPool pool)
    {
        GameObject ingredient = pool.Get();

        if (playerInventory.TryHold(ingredient))
        {
            Debug.Log("Ingredient picked up!");
            isSelecting = false;
        }
        else
        {
            pool.Return(ingredient);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ProximityInteractor>() != null)
        {
            isSelecting = false;
            Debug.Log("Left refrigerator.");
        }
    }
}