using static Define;

public abstract class BaseScene : InitBase
{
    public EScene SceneType { protected set; get; }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Init();

        return true;
    }

    public abstract void Clear();
}
