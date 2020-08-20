using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public LoadingProgression loadingProgression;
    public GameObject pauseButton;
    public GameObject generationPanel;
    public Image imagePreviewer;
    public PuzzleViewer puzzleViewer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Instance = null;
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchMainUI(bool active)
    {
        pauseButton.SetActive(active);
        generationPanel.SetActive(active);
    }

    public void ActivatePuzzleViewer(bool active, List<Texture2D> sprites)
    {
        imagePreviewer.transform.parent.gameObject.SetActive(active);
        puzzleViewer.gameObject.SetActive(active);
        if (sprites != null)
        {
            imagePreviewer.sprite = Sprite.Create(sprites[0], new Rect(0, 0, imagePreviewer.preferredWidth, imagePreviewer.preferredHeight), new Vector2());
            puzzleViewer.Initialize(sprites);
        }
    }

    public void SwitchLoaderProgression(bool active)
    {
        loadingProgression.gameObject.SetActive(active);
    }
}
