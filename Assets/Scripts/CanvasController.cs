using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public static CanvasController Instance { get; private set; }

    [SerializeField] private Sprite[] lifebarSprites = default;

    private Image lifebarImage;

    private void Awake()
    {
        lifebarImage = Utils.FindChildByNameRecursively(transform, "CalvinLifebar_Image").GetComponent<Image>();
        SetLifebar(.5f);
    }

    public void SetLifebar(float lifePercent)
    {
        lifebarImage.sprite = lifebarSprites[Mathf.FloorToInt(lifebarSprites.Length * lifePercent)];
    }
}
