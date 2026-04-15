using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cinemachine;
using DG.Tweening;
using UnityEngine.UIElements;

public class ResolutionManager : Singleton<ResolutionManager>
{
    public Camera MainCam { get; private set; }
    public Transform canvasBottomLeft, canvasTopRight;
    public Vector2 gameplayBottomLeft, gameplayTopRight;

    public bool Initialized { get; private set; }

    private void Start()
    {
        StartCoroutine(IEInit());
    }

    IEnumerator IEInit()
    {
        MainCam = Camera.main;
        yield return null;
        float orthoSize = MainCam.orthographicSize;

        Vector2 gameplaySize = gameplayTopRight - gameplayBottomLeft;
        Vector2 canvasSize = canvasTopRight.position - canvasBottomLeft.position;

        float gameplayAspectRatio = gameplaySize.x / gameplaySize.y;
        float canvasAspectRatio = canvasSize.x / canvasSize.y;

        bool calculateByWidth = gameplayAspectRatio > canvasAspectRatio;

        float multiplier = calculateByWidth ? gameplaySize.x / canvasSize.x : gameplaySize.y / canvasSize.y;
        Camera.main.orthographicSize = orthoSize * multiplier;
        Canvas.ForceUpdateCanvases();
        yield return null;

        canvasSize = canvasTopRight.position - canvasBottomLeft.position;
        Vector2 centerGameplay = gameplayBottomLeft + gameplaySize / 2;
        Vector2 centerCanvas = (Vector2)canvasBottomLeft.position + canvasSize / 2;
        Vector2 offset = centerCanvas - centerGameplay;

        MainCam.transform.position += (Vector3)offset;

        Initialized = true;
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(gameplayBottomLeft + (gameplayTopRight - gameplayBottomLeft) / 2,
            gameplayTopRight - gameplayBottomLeft);
    }
#endif
}