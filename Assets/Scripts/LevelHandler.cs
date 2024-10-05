using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private GameObject _loadingPage;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private int _count;

    private Coroutine _movementProcessCoroutine;

    private int Level
    {
        get => YG.YandexGame.savesData.Level;
        set
        {
            YG.YandexGame.savesData.Level = value;
            YG.YandexGame.SaveProgress();
        }
    }

    private int Stage
    {
        get => YG.YandexGame.savesData.Stage;
        set
        {
            YG.YandexGame.savesData.Stage = value;
            YG.YandexGame.SaveProgress();
        }
    }

    public void Init() 
    {
        _movementProcessCoroutine = StartCoroutine(MovementProcess());
        StartCoroutine(LoadingStartingScene()); 
    } 

    private IEnumerator LoadingStartingScene()
    {
        yield return SceneManager.LoadSceneAsync(GetScene(), LoadSceneMode.Additive);        
        Destroy(_loadingPage);
        //YG.YandexGame.GameReadyAPI(); 
        //вроде сейчас обязательно, но хз
    }

    private IEnumerator MovementProcess()
    {
        Vector3 target = GetPosition();

        while(_cameraTransform.position != target)
        {
            _cameraTransform.position = Vector3.MoveTowards(_cameraTransform.position, target, _movementSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void ReleaseCoroutine()
    {
        if(_movementProcessCoroutine != null)
        {
            StopCoroutine(_movementProcessCoroutine);
            _movementProcessCoroutine = null;
        }
    }

    private int GetScene()
    {
        return Level;
    }

    private Vector3 GetPosition()
    {
        return Stage switch
        {
            0 => Vector3.zero,
            1 => Vector3.forward * 100,
            2 => Vector3.forward * 200,
            3 => Vector3.forward * 300,
            _ => throw new System.NotImplementedException(),
        };
    }
}