using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    private NetworkController networkController = null;

    public void Awake() {
        networkController = new NetworkController("127.0.0.1", 11000);
        networkController.Send("");
    }

}
