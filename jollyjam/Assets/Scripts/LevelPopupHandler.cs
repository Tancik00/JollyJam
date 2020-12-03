using UnityEngine;

public class LevelPopupHandler : MonoBehaviour
{
    public bool isShowPopup;
    public int popupId;
    public PopupConfig popupConfig;

    private void Start()
    {
        if (isShowPopup)
        {
            if (!AppStateData.IsRestartButtonPressed)
            {
                AppStateData.IsPause = true;
                Instantiate(popupConfig.popups[popupId], Vector3.zero, Quaternion.identity);
            }
            else
            {
                AppStateData.IsPause = false;
            }
        }
    }
}
