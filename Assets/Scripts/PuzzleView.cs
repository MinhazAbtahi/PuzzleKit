using UnityEngine;
using UnityEngine.UI;

public class PuzzleView : MonoBehaviour
{
    private Sprite icon;
    private Button button;
    private Image image;
    private RuntimeGeneration runtimeGeneration;
    //private Texture2D selectedTexture;

    // Start is called before the first frame update
    void OnEnable()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        runtimeGeneration = FindObjectOfType<RuntimeGeneration>();
    }

    public void Assign(Sprite iconSprite)
    {
        icon = iconSprite;
        image.sprite = icon;
        button.onClick.AddListener(() => LoadImage());
    }

    public void Assign(Texture2D iconTexture)
    {
        icon = Sprite.Create(iconTexture, new Rect(0, 0, iconTexture.width, iconTexture.height), new Vector2());
        image.sprite = icon;
        button.onClick.AddListener(() => LoadImage());
        button.onClick.AddListener(() => runtimeGeneration.OnImageSelect(iconTexture));
    }

    public void LoadImage()
    {

    }
}
