using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFinishInsert : Insert
{
    protected override void SetListeners()
    {
        Button nextLevel = GameObject.Find("ButtonLF").GetComponent<Button>();
        nextLevel.onClick.AddListener(() =>
        {
            ScenesHandler.Instance.NextScene();
        });
    }
}
