using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using Cysharp.Threading.Tasks;
using static Define;

public class SceneManagerEx
{
    private CancellationTokenSource cts = null;

    public BaseScene CurrScene { get; private set; }

    private EScene nextScene = EScene.Unknown;
    private int loadingProgress = 100; // 0 ~ 100

    public void SetCurrentScene(BaseScene currScene)
    {
        CurrScene = currScene;
    }

    public bool IsCompleteLoadingScene() => loadingProgress == 100;
    public int GetLoadingSceneProgress() => loadingProgress;

    public void LoadScene(Define.EScene type)
    {
        if(cts != null)
        {
            Debug.LogError("중복된 씬로딩 요청");
            return;
        }

        nextScene = type;
        UIFadeEffectParam param = new UIFadeEffectParam(IsCompleteLoadingScene, LoadSceneAsync);
        Managers.UI.OpenPopupUI<UI_FadeEffectPopup>(param);
    }

    public void LoadSceneAsync()
    {
        Managers.Clear();
        UniLoadSceneAsync(nextScene).Forget();
    }

    private async UniTaskVoid UniLoadSceneAsync(Define.EScene type)
    {
        cts = new CancellationTokenSource();
        AsyncOperation AsyncLoad = SceneManager.LoadSceneAsync(GetSceneName(type));

        loadingProgress = 0;
        while (!AsyncLoad.isDone)
        {
            loadingProgress = (int)(AsyncLoad.progress * 100.0f);

            await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken: cts.Token);
        }
        loadingProgress = 100;

        cts?.Dispose();
        cts = null;
    }

    private string GetSceneName(Define.EScene type)
    {
        string name = System.Enum.GetName(typeof(Define.EScene), type);
        return name;
    }

    public void Clear()
    {
        CurrScene.Clear();
    }
}
