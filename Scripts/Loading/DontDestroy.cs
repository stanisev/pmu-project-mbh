using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{

 // #region Singleton Linker
 //
 //  public static DontDestroy instance;
 //
 //  private void Awake()
 //  {
 //      if (instance == null)
 //      {
 //          instance = this;
 //          DontDestroyOnLoad(gameObject);
 //      }
 //      else
 //      {
 //          Destroy(gameObject);
 //      }
 //  }
 // #endregion

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
