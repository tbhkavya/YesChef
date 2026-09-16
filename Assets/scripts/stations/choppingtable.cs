using UnityEngine;

public class ChoppingTable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameManager gameManager;

    [Header("Player")]
    [SerializeField] private PlayerInventory playerInventory;

    [Header("UI")]
    [SerializeField] private GameObject timerUI;
    [SerializeField] private TMPro.TMP_Text timerText;

    private GameObject choppingIngredient;
    private bool isChopping;

    private float choppingDuration = 2f;
    private float choppingTimer;


    public void Interact()
    {
        if (!gameManager.IsGameRunning)
            return;
        
        if (isChopping)
        {
            Debug.Log("Already chopping!");
            return;
        }

 
        if (choppingIngredient != null)
        {
            if (playerInventory.IsHolding)
            {
                Debug.Log("Hands are full!");
                return;
            }

            PickUpVegetable();
            return;
        }

         
        if (!playerInventory.IsHolding)
        {
            Debug.Log("You need a vegetable in your hand.");
            return;
        }

        GameObject ingredient =
            playerInventory.GetHeldIngredient();

        Ingredient ingredientData =
            ingredient.GetComponent<Ingredient>();

        
        if (ingredientData == null ||
     ingredientData.Type != IngredientType.Vegetable)
        {
            Debug.Log("Only vegetables can be chopped here.");
            return;
        }

        if (ingredientData.IsPrepared)
        {
            Debug.Log("This vegetable is already chopped.");
            return;
        }
         
        PlaceVegetable(ingredient);
    }

    private void PlaceVegetable(GameObject ingredient)
    {
        choppingIngredient = ingredient;

        
        playerInventory.ClearHand();

        
        Collider tableCollider = GetComponent<Collider>();
        Collider ingredientCollider =
            choppingIngredient.GetComponent<Collider>();

  
        float tableTop = tableCollider.bounds.max.y;

    
        float ingredientHalfHeight =
            ingredientCollider.bounds.extents.y;

         
        Vector3 position = transform.position;

        position.y = tableTop + ingredientHalfHeight;

        
        choppingIngredient.transform.SetParent(null, true);
        choppingIngredient.transform.position = position;
        choppingIngredient.transform.rotation = Quaternion.identity;

         
        choppingIngredient.transform.SetParent(transform, true);

        
        isChopping = true;
        choppingTimer = choppingDuration;
 
        if (timerUI != null)
        {
            timerUI.SetActive(true);
        }

        UpdateTimerUI();

        Debug.Log("Chopping started!");
    }


    private void Update()
    {
        if(gameManager == null || !gameManager.IsGameRunning)
                return;
        if (!isChopping)
            return;

        choppingTimer -= Time.deltaTime;

        UpdateTimerUI();

        if (choppingTimer <= 0f)
        {
            FinishChopping();
        }
    }


    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text =
                "CHOPPING\n" +
                Mathf.Max(0f, choppingTimer).ToString("F1") +
                "s";
        }
    }


    private void FinishChopping()
    {
        choppingTimer = 0f;
        isChopping = false;

        
        Ingredient ingredient =
            choppingIngredient.GetComponent<Ingredient>();

        ingredient.SetPrepared(true);

         
        if (timerUI != null)
        {
            timerUI.SetActive(false);
        }

        Debug.Log("Vegetable chopped!");
    }


    private void PickUpVegetable()
    {
        GameObject ingredient = choppingIngredient;

        choppingIngredient = null;
 
        playerInventory.TryHold(ingredient);

        Debug.Log("Picked up chopped vegetable!");
    }
}