using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using System.Threading.Tasks;

public class FirebaseDatabaseManager : MonoBehaviour
{
    public static FirebaseDatabaseManager Instance;

    [SerializeField] string databaseUrl;
    DatabaseReference databaseRef;

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

        if (string.IsNullOrEmpty(databaseUrl))
        {
            Debug.LogError("You forgot to add you database URL in the inspector! Please check it!");
        }
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(databaseUrl);
        // Get the root reference location of the database.
        databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    void Start()
    {

    }

    #region POST (add new) levels
    /// <summary> Add new the level with a generated key </summary>
    public async Task RegisterUserAsync(User user, Action OnSuccess, Action<AggregateException> OnError)
    {
        string json = JsonConvert.SerializeObject(user);
        AggregateException exception = null;
        //Push creates a new unique key automatically for our level
        await databaseRef.Child("users").Push().SetRawJsonValueAsync(json).ContinueWith(
            task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    exception = task.Exception;
                }
            }
            );
        if (exception != null)
        {
            OnError(exception);
        }
        else
        {
            OnSuccess();
        }
    }
    #endregion


    #region GET all contents of a child node
    /// <summary>
    /// Get all Users from database
    /// </summary>
    /// <param name="OnSuccess"></param>
    /// <param name="OnError"></param>
    public async void GetUsersAsync(Action<List<User>> OnSuccess, Action<AggregateException> OnError)
    {
        AggregateException exception = null;
        List<User> users = new List<User>();
        await databaseRef.Child("users")
         .GetValueAsync().ContinueWith(task =>
         {
             if (task.IsFaulted || task.IsCanceled)
             {
                 exception = task.Exception;
             }
             else if (task.IsCompleted)
             {
                 DataSnapshot snapshot = task.Result;
                 foreach (var item in snapshot.Children)
                 {
                     User tempUser = JsonConvert.DeserializeObject<User>(item.GetRawJsonValue());
                     tempUser.DatabaseKey = item.Key;
                     users.Add(tempUser);
                 }
             }
         });

        if (exception != null)
        {
            OnError(exception);
        }
        else
        {
            OnSuccess(users);
        }
    }

    /// <summary>
    /// Get all Puzzles from database
    /// </summary>
    /// <param name="OnSuccess"></param>
    /// <param name="OnError"></param>
    public async void GetPuzzlesAsync(Action<List<Puzzle>> OnSuccess, Action<AggregateException> OnError)
    {
        AggregateException exception = null;
        List<Puzzle> puzzles = new List<Puzzle>();
        await databaseRef.Child("puzzles")
         .GetValueAsync().ContinueWith(task =>
         {
             if (task.IsFaulted || task.IsCanceled)
             {
                 exception = task.Exception;
             }
             else if (task.IsCompleted)
             {
                 DataSnapshot snapshot = task.Result;
                 foreach (var item in snapshot.Children)
                 {
                     Puzzle tempPuzzle = JsonConvert.DeserializeObject<Puzzle>(item.GetRawJsonValue());
                     tempPuzzle.DatabaseKey = item.Key;
                     puzzles.Add(tempPuzzle);
                 }
             }
         });

        if (exception != null)
        {
            OnError(exception);
        }
        else
        {
            OnSuccess(puzzles);
        }
    }
    #endregion

}
