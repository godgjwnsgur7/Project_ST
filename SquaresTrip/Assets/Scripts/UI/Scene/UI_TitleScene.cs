using System.Collections;
using TMPro;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UI_TitleScene : UI_BaseScene
{
    private CancellationTokenSource cts = null;

    [SerializeField] private TextMeshProUGUI blinkText;

    private bool textEffectLock = false;

#if UNITY_EDITOR
    private void Reset()
    {
        blinkText = Util.FindChild<TextMeshProUGUI>(gameObject, "BlinkText", true);
    }
#endif

    private void Start()
    {
        coBlinkEffectToText = StartCoroutine(CoBlinkEffectToText(20));
    }

    private void OnDisable()
    {
        if(cts != null)
        {
            cts.Dispose();
        }
    }

    #region OnClickEvent
    public void OnClickGoToGameScene()
    {
        Managers.Scene.LoadScene(Define.EScene.GameScene);
    }
    #endregion

    Coroutine coBlinkEffectToText = null;
    private IEnumerator CoBlinkEffectToText(int fadePercent)
    {
        textEffectLock = true;
        Color color = new Color(1, 1, 1, 1);
        float runTIme;
        float duration = 1.0f;
        float fadeValue = fadePercent * 0.01f;

        while (textEffectLock)
        {
            runTIme = 0.0f;
            color.a = 1.0f;
            blinkText.color = color;
            while (runTIme < duration)
            {
                runTIme += Time.deltaTime;
                color.a = Mathf.Lerp(1.0f, fadeValue, runTIme / duration);
                blinkText.color = color;
                yield return null;
            }

            runTIme = 0.0f;
            color.a = 0.5f;
            blinkText.color = color;
            while (runTIme < duration)
            {
                runTIme += Time.deltaTime;
                color.a = Mathf.Lerp(fadeValue, 1.0f, runTIme / duration);
                blinkText.color = color;
                yield return null;
            }
        }

        textEffectLock = false;
        coBlinkEffectToText = null;
    }
}
