using UnityEngine;

public class EditScene : BaseScene
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        SceneType = Define.EScene.Unknown;

        return true;
    }

    public override void Clear()
    {

    }
}
