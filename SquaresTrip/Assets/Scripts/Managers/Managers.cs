using TMPro;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static bool Initialized { get; protected set; } = false;

    private static Managers s_instance;
    private static Managers Instance { get { Init(); return s_instance; } }

    #region Contents
    private GameManager _game = new GameManager();
    private ObjectManager _object = new ObjectManager();
    private SteamManager _steam = new SteamManager();
    
    public static GameManager Game { get { return Instance?._game; } }
    public static ObjectManager Object { get { return Instance?._object; } }
    public static SteamManager Steam { get { return Instance?._steam; } }
    #endregion

    #region Core
    private DataManager _data = new DataManager();
    private InputManager _input = null; // Init()에서 초기화
    private ResourceManager _resource = new ResourceManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    private UIManager _ui = new UIManager();

    public static DataManager Data { get { return Instance?._data; } }
    public static InputManager Input { get { return Instance?._input; } }
    public static ResourceManager Resource { get { return Instance?._resource; } }
    public static SceneManagerEx Scene { get { return Instance?._scene; } }
    public static UIManager UI { get { return Instance?._ui; } }
    #endregion

    public static void Init()
    {
        if (s_instance == null && Initialized == false)
        {
            Initialized = true;

            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);

            s_instance = go.GetComponent<Managers>();

            s_instance._data.Init();
            GameObject inputObj = new GameObject { name = "Input" };
            inputObj.transform.parent = go.transform;
            s_instance._input = inputObj.AddComponent<InputManager>();
            s_instance._input.Init();
            s_instance._ui.Init();
        }
    }
    
    public static void Clear()
    {

    }

    private Managers() { }
}
