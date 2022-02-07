using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private MeshRenderer meshRenderer;

    public void Awake() {
        var teamColor = Random.ColorHSV(0.0f, 1.0f, 0.8f, 1.0f);
        meshRenderer.material.color = teamColor;
    }
}
