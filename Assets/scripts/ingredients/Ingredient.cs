using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private IngredientType ingredientType;

    [Header("Visuals")]
    [SerializeField] private Renderer ingredientRenderer;
    [SerializeField] private Material rawMaterial;
    [SerializeField] private Material preparedMaterial;

    public IngredientType Type => ingredientType;

    public bool IsPrepared { get; private set; }


    public void SetPrepared(bool prepared)
    {
        IsPrepared = prepared;

        if (ingredientRenderer == null)
            return;

        if (prepared && preparedMaterial != null)
        {
            ingredientRenderer.material = preparedMaterial;
        }
        else if (!prepared && rawMaterial != null)
        {
            ingredientRenderer.material = rawMaterial;
        }
    }

    public int GetValue()
    {
        switch (ingredientType)
        {
            case IngredientType.Vegetable:
                return 20;

            case IngredientType.Cheese:
                return 10;

            case IngredientType.Meat:
                return 30;
        }

        return 0;
    }
    public void ResetIngredient()
    {
        SetPrepared(false);
    }
}