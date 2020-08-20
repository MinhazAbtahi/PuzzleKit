//-----------------------------------------------------------------------------------------------------	
//  Simple demo script to help in puzzle-generation demonstration
//-----------------------------------------------------------------------------------------------------	
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class RuntimeGeneration : MonoBehaviour 
{
    public bool useUrl;
    public TMP_InputField urlInputField;
    public Image imagePreview;
    public List<Texture2D> images;
    public Texture2D image;                         // Will be used as main puzzle image
    public bool generateBackground = true;          // Automatically generate puzzle background from the source image
    public bool alignWithCamera = true;             // Automatically align puzzle/background with camera center
    public bool clearOldSaves = true;               // Clear existing Save data data during generation
    [TextArea]
    public string pathToImage;                      // pathToImage should starts from "http://"(for online image)  or  from "file://" (for local) 

    public PuzzleGenerator_Runtime puzzleGenerator;
	public GameController gameController;
	public Text rows;
	public Text cols;


    void Start()
    {
        //image = images[0];    
    }

    public void OnImageSelect(int index)
    {
        image = images[index];
        imagePreview.sprite = Sprite.Create(image, new Rect(0, 0, imagePreview.preferredWidth, imagePreview.preferredHeight), new Vector2());
    }

    public void OnImageSelect(Texture2D selectedTexture)
    {
        image = selectedTexture;
        imagePreview.sprite = Sprite.Create(image, new Rect(0, 0, imagePreview.preferredWidth, imagePreview.preferredHeight), new Vector2());
    }

    //============================================================================================================================================================
    public void GeneratePuzzle ()
	{
        if (urlInputField.text == "")
        {
            useUrl = false;
        }
        else
        {
            useUrl = true;
            pathToImage = urlInputField.text;
        }

		if (puzzleGenerator == null || gameController == null) 
		{
			Debug.LogWarning ("Please assign puzzleGenerator and gameController to " + gameObject.name + "RuntimeGenerator");
			return;
		}

        gameController.enabled = false;

        //Delete previously generated puzzle
        if (gameController.puzzle != null)
            Destroy(gameController.puzzle.gameObject);
        if (gameController.background != null)
            Destroy(gameController.background.gameObject);

        if (useUrl)
        {
            puzzleGenerator.CreateFromExternalImage(pathToImage);
        }
        else
        {
            gameController.puzzle = puzzleGenerator.CreatePuzzleFromImage(image);
            //gameController.puzzle = puzzleGenerator.CreatePuzzleFromImage(puzzleGenerator.images[1]);
        }

        //if (!image)
        //    puzzleGenerator.CreateFromExternalImage(pathToImage);               
        //else
        //    gameController.puzzle = puzzleGenerator.CreatePuzzleFromImage(image);


        StartCoroutine(StartPuzzleWhenReady());
    }

    //-----------------------------------------------------------------------------------------------------
    IEnumerator StartPuzzleWhenReady()
    {
        while (puzzleGenerator.puzzle == null)
        {
            yield return null;
        }

        if (clearOldSaves)
        { 
           PlayerPrefs.DeleteKey(puzzleGenerator.puzzle.name);
           PlayerPrefs.DeleteKey(puzzleGenerator.puzzle.name + "_Positions");
        }

        gameController.puzzle = puzzleGenerator.puzzle;

        // Generate backround if needed
        if (generateBackground)
            gameController.background = puzzleGenerator.puzzle.GenerateBackground(puzzleGenerator.GetSourceImage());

        // Align with Camera if needed
        if (alignWithCamera)
            puzzleGenerator.puzzle.AlignWithCameraCenter(gameController.gameCamera);

 
        gameController.enabled = true;
    }

    //-----------------------------------------------------------------------------------------------------	
    public void SetRows (float _amount) 
	{
		if (puzzleGenerator != null)
			puzzleGenerator.rows = (int)_amount;

		if (rows != null)
			rows.text = ((int)_amount).ToString();		
	}

	//-----------------------------------------------------------------------------------------------------	
	public void SetCols (float _amount) 
	{
		if (puzzleGenerator != null)
			puzzleGenerator.cols = (int)_amount;

		if (cols != null)
			cols.text = ((int)_amount).ToString();
	}

	//-----------------------------------------------------------------------------------------------------	
}