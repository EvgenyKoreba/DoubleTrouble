using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] GameObject chooseLevelButtonGO;
    [SerializeField] GameObject backButtonGO;
    [SerializeField] GameObject exitButtonGO;
    [SerializeField] GameObject[] levelsButtonGO;
    [SerializeField] Scene[] levels;

    void Start()
    {
        Button chooseLevelButton = chooseLevelButtonGO.GetComponent<Button>();
        chooseLevelButton.onClick.AddListener(ChooseLevel);
    }


    void Update()
    {

    }

    public void ChooseLevel()
    {
        chooseLevelButtonGO.SetActive(false);
        exitButtonGO.SetActive(false);
        foreach (GameObject levelButtons in levelsButtonGO)
        {
            levelButtons.SetActive(true);
        }

    }


    private void BackToMenu()
    {
        chooseLevelButtonGO.SetActive(true);
        exitButtonGO.SetActive(true);
        foreach (GameObject levelButtons in levelsButtonGO)
        {
            levelButtons.SetActive(false);
        }
    }
}
