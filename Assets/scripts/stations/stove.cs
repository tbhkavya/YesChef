using UnityEngine;

public class Stove : MonoBehaviour, IInteractable
{
    [Header("Player")]
    [SerializeField] private PlayerInventory playerInventory;

    [Header("Stove Slots")]
    [SerializeField] private Transform slot1;
    [SerializeField] private Transform slot2;

    private GameObject cookingIngredient1;
    private GameObject cookingIngredient2;

    private bool isCooking1;
    private bool isCooking2;
    [Header("UI")]
    [SerializeField] private GameObject timerUI1;
    [SerializeField] private TMPro.TMP_Text timerText1;

    [SerializeField] private GameObject timerUI2;
    [SerializeField] private TMPro.TMP_Text timerText2;
    private float cookingDuration = 6f;
    private float cookingTimer1;
    private float cookingTimer2;
    [SerializeField] private GameManager gameManager;

    public void Interact()
    {
        if (!gameManager.IsGameRunning)
            return;

         
        if (playerInventory.IsHolding)
        {
            GameObject ingredient =
                playerInventory.GetHeldIngredient();

            Ingredient ingredientData =
                ingredient.GetComponent<Ingredient>();

             
            if (ingredientData == null ||
                ingredientData.Type != IngredientType.Meat)
            {
                Debug.Log("Only meat can be cooked here.");
                return;
            }

             
            if (ingredientData.IsPrepared)
            {
                Debug.Log("This meat is already cooked.");
                return;
            }

             
            if (!isCooking1 && cookingIngredient1 == null)
            {
                StartCooking(ingredient, 1);
                return;
            }

            if (!isCooking2 && cookingIngredient2 == null)
            {
                StartCooking(ingredient, 2);
                return;
            }

            Debug.Log("Both stove slots are occupied!");
            return;
        }


         

        if (cookingIngredient1 != null && !isCooking1)
        {
            PickUpIngredient(1);
            return;
        }

         
        if (cookingIngredient2 != null && !isCooking2)
        {
            PickUpIngredient(2);
            return;
        }

        Debug.Log("No cooked meat is ready.");
    }


    private void StartCooking(GameObject ingredient, int slot)
    {
        playerInventory.ClearHand();

        ingredient.transform.SetParent(null, true);

        Transform targetSlot =
            slot == 1 ? slot1 : slot2;

        ingredient.transform.position =
            targetSlot.position;

        ingredient.transform.rotation =
            Quaternion.identity;

        ingredient.transform.SetParent(
            targetSlot,
            true
        );

        if (slot == 1)
        {
            cookingIngredient1 = ingredient;
            isCooking1 = true;
            if (timerUI1 != null)
                timerUI1.SetActive(false);
            cookingTimer1 = cookingDuration;
        }
        else
        {
            cookingIngredient2 = ingredient;
            isCooking2 = true;
            if (timerUI2 != null)
                timerUI2.SetActive(false);
            cookingTimer2 = cookingDuration;
        }

        Debug.Log("Meat started cooking in slot " + slot);

        if (slot == 1)
        {
            if (timerUI1 != null)
                timerUI1.SetActive(true);

            UpdateTimerUI1();
        }
        else
        {
            if (timerUI2 != null)
                timerUI2.SetActive(true);

            UpdateTimerUI2();
        }
    }


    private void Update()
    {
        if(gameManager == null || !gameManager.IsGameRunning)
                return;
        if (isCooking1)
        {
            cookingTimer1 -= Time.deltaTime;

            UpdateTimerUI1();

            if (cookingTimer1 <= 0f)
            {
                FinishCooking(1);
            }
        }

        if (isCooking2)
        {
            cookingTimer2 -= Time.deltaTime;

            UpdateTimerUI2();

            if (cookingTimer2 <= 0f)
            {
                FinishCooking(2);
            }
        }
    }
    private void UpdateTimerUI1()
    {
        if (timerText1 != null)
        {
            timerText1.text =
                "COOKING\n" +
                Mathf.Max(0f, cookingTimer1).ToString("F1") +
                "s";
        }
    }


    private void UpdateTimerUI2()
    {
        if (timerText2 != null)
        {
            timerText2.text =
                "COOKING\n" +
                Mathf.Max(0f, cookingTimer2).ToString("F1") +
                "s";
        }
    }

    private void FinishCooking(int slot)
    {
        if (slot == 1)
        {
            cookingTimer1 = 0f;
            isCooking1 = false;

            Ingredient ingredient =
                cookingIngredient1.GetComponent<Ingredient>();

            ingredient.SetPrepared(true);

            Debug.Log("Meat in slot 1 is cooked!");
        }
        else
        {
            cookingTimer2 = 0f;
            isCooking2 = false;

            Ingredient ingredient =
                cookingIngredient2.GetComponent<Ingredient>();

            ingredient.SetPrepared(true);

            Debug.Log("Meat in slot 2 is cooked!");
        }
    }


    private void PickUpIngredient(int slot)
    {
        GameObject ingredient;

        if (slot == 1)
        {
            ingredient = cookingIngredient1;
            cookingIngredient1 = null;
        }
        else
        {
            ingredient = cookingIngredient2;
            cookingIngredient2 = null;
        }

        playerInventory.TryHold(ingredient);

        Debug.Log("Picked up cooked meat!");
    }
}