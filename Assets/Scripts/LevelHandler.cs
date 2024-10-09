using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using YG;

public class LevelHandler : MonoBehaviour
{
    public UnityEvent OnLevelComplated;
    public UnityEvent OnStageComplated;
    public UnityEvent OnEndMovement;

    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private GameObject _loadingPage;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private int _count;

    private readonly List<LevelManager> LevelList = new(); 

    private Coroutine _movementProcessCoroutine;
    private Vector3 _currentLevelPosition = new (0, 0, -400);

    private bool _hasNewLevel = false;
    private int Level
    {
        get => YandexGame.savesData.Level;
        set
        {
            YandexGame.savesData.Level = value;
            YandexGame.SaveProgress();
            OnLevelComplated?.Invoke();
        }
    }
    private int Stage
    {
        get => YandexGame.savesData.Stage;
        set
        {
            int stage = value;
            if (stage > 3)
            {
                stage = 0;
                Level++;
                _hasNewLevel = true;
                YandexGame.savesData.Stage = stage;
                YandexGame.SaveProgress();
            }
            else 
            {
                YandexGame.savesData.Stage = stage;
                YandexGame.SaveProgress();
                MoveNextStage();
            }
            OnStageComplated?.Invoke();
        }
    }

    public void Init() 
    {
        _movementProcessCoroutine = StartCoroutine(LoadingStartingScene());
        Game.Action.OnWin += () => { Stage++; }; 
    } 

    private IEnumerator LoadingStartingScene()
    {
        _cameraTransform.position = GetStartPosition();
        yield return SceneManager.LoadSceneAsync(GetScene(Level), LoadSceneMode.Additive);
        yield return SceneManager.LoadSceneAsync(GetScene(Level + 1), LoadSceneMode.Additive);
        LevelList[0].Activate(Stage);
        Destroy(_loadingPage);
    }

    public void AddLevel(LevelManager level)
    {
        LevelList.Add(level);
        level.SetPosition(GetLevelPosition());
    }

    public void MoveNextStage()
    {           
        ReleaseCoroutine();
        _movementProcessCoroutine = StartCoroutine(MovementProcess());
    }

    private IEnumerator MovementProcess()
    {
        if (_hasNewLevel) LevelList[1].Activate(0);
        else LevelList[0].Activate(Stage);

        Vector3 target = _cameraTransform.position + Vector3.forward * 100;

        while(_cameraTransform.position != target)
        {
            _cameraTransform.position = Vector3.MoveTowards(_cameraTransform.position, target, _movementSpeed * Time.deltaTime);
            yield return null;
        }

        OnEndMovement?.Invoke();

        if(_hasNewLevel)
        {
            LevelList[0].Deactivate(3);
            yield return SceneManager.LoadSceneAsync(GetScene(Level + 1), LoadSceneMode.Additive);
            yield return SceneManager.UnloadSceneAsync(LevelList[0].ID);
            LevelList.RemoveAt(0);
            _hasNewLevel = false;
        }
        else LevelList[0].Deactivate(Stage - 1);
    }

    private void ReleaseCoroutine()
    {
        if(_movementProcessCoroutine != null)
        {
            StopCoroutine(_movementProcessCoroutine);
            _movementProcessCoroutine = null;
        }
    }

    private int GetScene(int lvl)
    {
        int level = lvl;

        while(level > _count)
        {
            level -= _count;
        }
        return level;
    }

    private Vector3 GetStartPosition()
    {
        return Stage switch
        {
            0 => Vector3.zero,
            1 => Vector3.forward * 100,
            2 => Vector3.forward * 200,
            3 => Vector3.forward * 300,
            _ => throw new NotImplementedException(),
        };
    }

    public Vector3 GetLevelPosition()
    {
        _currentLevelPosition.z += 400;
        return _currentLevelPosition;
    }
}