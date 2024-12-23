using UnityEngine;
using UnityEngine.InputSystem;

public class RebindSaveLoad : MonoBehaviour
{
    public InputActionAsset actions;

    public void OnEnable()
    {
        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
            actions.LoadBindingOverridesFromJson(rebinds);
    }

    public void OnDone()
    {
        var rebinds = actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
    }
    public void OnDisable()
    {
        OnDone();
    }

    public void OnRestoreDefaultKeyBind()
    {
        foreach(InputActionMap action in actions.actionMaps)
        {
            action.RemoveAllBindingOverrides();
        }
        OnDone();
    }
}
