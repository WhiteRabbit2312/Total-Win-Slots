using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject menuPanel;

    [SerializeField] private List<GameObject> slide;

    private int tutorSlideNumber;
    // Start is called before the first frame update

    private void Awake()
    {
        tutorSlideNumber = 0;
        tutorialPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayButton()
    {
        SceneManager.LoadScene(2);
    }

    public void TutorialButton()
    {
        tutorialPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    private void FlipSlide()
    {
        for (int i = 0; i < slide.Count; ++i)
        {
            if (i == tutorSlideNumber)
                slide[i].SetActive(true);
            else slide[i].SetActive(false);
        }

    }

    public void Left() 
    { 
        if(tutorSlideNumber > 0) 
            tutorSlideNumber--;

        FlipSlide();
    }
   

    public void Right() 
    { 
        if (tutorSlideNumber < 2) 
            tutorSlideNumber++;

        FlipSlide();
    }

}
