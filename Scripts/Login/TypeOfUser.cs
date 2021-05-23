using Firebase.Database;
using UnityEngine;

public class TypeOfUser : MonoBehaviour
{
    #region Singleton

    public static TypeOfUser instance;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of TypeOfUser found!!!");
        }
        instance = this;



    }
    #endregion

    private static string userType;

    private static string username;

    private static DatabaseReference db;

    public void SET_USERNAME(string type)
    {
        username = type;
    }
    public string GET_USERNAME()
    {
        return username;
    }

    public void SET_USER_TYPE(string type)
    {
        userType = type;
    }
    public string GET_USER_TYPE()
    {
        return userType;
    }

    public DatabaseReference GET_DB()
    {
        return db;
    }

    public void SET_DB(DatabaseReference _db)
    {
        db = _db;
    }
    public string CREATE_DEFAULT()
    {
        string defaultUsername = PlayerPrefs.GetString("defaultUsername");
        string codesequence = null;

        if (defaultUsername.Equals(null) || defaultUsername.Equals("")){
           for(int i = 0; i < 5; i++)
            {
                int random = Random.Range(0, 9);
                codesequence += random.ToString();
            }
            defaultUsername = "default" + codesequence;
            PlayerPrefs.SetString("defaultUsername", defaultUsername);
        }
        Debug.Log(defaultUsername);
        userType = defaultUsername;

        return userType;
    }

    
}
