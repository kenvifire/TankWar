using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Option : MonoBehaviour
{

    public Transform option1;
    public Transform option2;
    protected Joystick joystick;
    protected Joybutton joybutton;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = option1.position;
        joystick = FindObjectOfType<Joystick>();
        joystick.AxisOptions = AxisOptions.Vertical;

        joybutton = FindObjectOfType<Joybutton>();

    }

    // Update is called once per frame
    void Update()
    {
        if (joystick.Vertical > 0)
        {
            transform.position = option1.position;
        } else if (joystick.Vertical < 0)
        {
            transform.position = option2.position;
        }

        if (joybutton.isPressed)
        {
            StartGame();
        }

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
