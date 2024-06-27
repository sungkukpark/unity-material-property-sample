using System.Collections;
using UnityEngine;

public class MaterialPropertyBlockSample : MonoBehaviour
{
    private const int MaxColorValue = 255;

    private static readonly int ColorPropertyID = Shader.PropertyToID("_BaseColor");

    public float TimeBetweenColorChanges = 0.5f;

    public bool IsRunning
    {
        set
        {
            isRunning = value;

            if (isRunning)
            {
                StopRandomizeColorsCoroutine();
                randomizeColorsCoroutine = StartCoroutine(RandomizeSkinnedMeshRendererColors());
            }
            else
            {
                StopRandomizeColorsCoroutine();
            }
        }
    }

    private void StopRandomizeColorsCoroutine()
    {
        if (randomizeColorsCoroutine != null)
        {
            StopCoroutine(randomizeColorsCoroutine);
        }
    }

    private bool isRunning;

    [SerializeField] private SkinnedMeshRenderer[] SkinnedMeshRenderers;

    private Coroutine randomizeColorsCoroutine;

    private static Color CreateRandomColor()
    {
        int r = Random.Range(0, MaxColorValue + 1);
        int g = Random.Range(0, MaxColorValue + 1);
        int b = Random.Range(0, MaxColorValue + 1);

        return new Color(r / (float)MaxColorValue, g / (float)MaxColorValue, b / (float)MaxColorValue);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        IsRunning = true;
    }

    private IEnumerator RandomizeSkinnedMeshRendererColors()
    {
        while (isRunning)
        {
            foreach (var meshRenderer in SkinnedMeshRenderers)
            {
                MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
                materialPropertyBlock.SetColor(ColorPropertyID, CreateRandomColor());

                meshRenderer.SetPropertyBlock(materialPropertyBlock);
            }

            yield return new WaitForSeconds(TimeBetweenColorChanges);
        }
    }
}