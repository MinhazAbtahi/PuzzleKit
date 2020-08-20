using System;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseTest : MonoBehaviour
{
    public static DatabaseTest Instance;

    private FirebaseDatabaseManager databaseManager;
    public List<string> imagePaths;
    public PuzzleGenerator_Runtime puzzleGenerator;

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
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        databaseManager = FirebaseDatabaseManager.Instance;
        imagePaths = new List<string>();
        OnGetPuzzlesButtonClicked();
    }

    #region POST (add new to database)
    public void RegisterButtonClicked()
    {
        User user = new User("user420", "user420@mail.com", "m", "01-01-1999", "CA");
        databaseManager.RegisterUserAsync(user, OnUploadLevelSuccess, OnUploadLevelFailed);
    }

    void OnUploadLevelSuccess()
    {
        Debug.Log("Upload success");
    }

    void OnUploadLevelFailed(AggregateException exception)
    {
        Debug.LogError(exception.ToString());
    }
    #endregion


    #region GET (read from database)
    /// <summary>
    /// Get all Puzzles
    /// </summary>
    public void OnGetPuzzlesButtonClicked()
    {
        databaseManager.GetPuzzlesAsync(OnGetPuzzleSuccess, OnGetPuzzleFailed);
    }

    void OnGetPuzzleSuccess(List<Puzzle> result)
    {
        if (result.Count == 0)
        {
            Debug.Log("Call succeed, but no results found in the database!");
        }
        else
        {
            foreach (var puzzle in result)
            {
                //Debug.Log("Successfully retrieved puzzle (dbKey): " + puzzle.DatabaseKey);
                //Debug.Log("Name: " + puzzle.name);
                //Debug.Log("Category: " + puzzle.category);
                //Debug.Log("Image Url: " + puzzle.image);
                //Debug.Log("User Count: " + puzzle.user_count);
                imagePaths.Add(puzzle.image);
            }
        }
        puzzleGenerator.CreateFromExternalImage(imagePaths);
    }

    void OnGetPuzzleFailed(AggregateException exception)
    {
        Debug.LogError(exception.ToString());
    }

    /// <summary>
    /// Get all USers
    /// </summary>
    /// <param name="result"></param>
    public void OnGetUsersButtonClicked()
    {
        databaseManager.GetUsersAsync(OnGetUserSuccess, OnGetUserFailed);
    }

    void OnGetUserSuccess(List<User> result)
    {
        if (result.Count == 0)
        {
            Debug.Log("Call succeed, but no results found in the database!");
        }
        else
        {
            foreach (var user in result)
            {
                Debug.Log("Successfully retrieved User (dbKey): " + user.DatabaseKey);
                Debug.Log("Name: " + user.name);
                Debug.Log("Email: " + user.email);
                Debug.Log("City: " + user.city_residence);
                Debug.Log("DOB: " + user.dob);
                Debug.Log("Sex: " + user.sex);
            }
        }
    }

    void OnGetUserFailed(AggregateException exception)
    {
        Debug.LogError(exception.ToString());
    }
    #endregion

}
