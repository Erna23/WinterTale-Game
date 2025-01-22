using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance;
    private void Awake() {
        if(instance == null){
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }
    }
}
