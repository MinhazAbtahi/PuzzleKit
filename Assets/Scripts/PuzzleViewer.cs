using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleViewer : MonoBehaviour
{
    public List<Texture2D> images;

    public List<PuzzleViewData> puzzleViews;
    public Transform contentHolder;
    public Button buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
    {
        foreach (var game in puzzleViews)
        {
            var button = Instantiate(buttonPrefab, contentHolder);
            contentHolder.GetComponent<GridLayoutGroup>().enabled = false;
            contentHolder.GetComponent<GridLayoutGroup>().enabled = true;
            button.GetComponent<PuzzleView>().Assign(game.icon);
        }
    }

    public void Initialize(List<Texture2D> sprites)
    {
        //puzzleViews = new List<PuzzleViewData>(sprites.Count);

        //for (int i = 0; i < sprites.Count; i++)
        //{
        //    puzzleViews[i].icon = sprites[i];
        //}

        foreach (var icon in sprites)
        {
            Button button = Instantiate(buttonPrefab, contentHolder) as Button;
            contentHolder.GetComponent<GridLayoutGroup>().enabled = false;
            contentHolder.GetComponent<GridLayoutGroup>().enabled = true;
            button.GetComponent<PuzzleView>().Assign(icon);
        }
    }
}

[System.Serializable]
public class PuzzleViewData
{
    //public string name;
    //public string category;
    public Sprite icon;
}
