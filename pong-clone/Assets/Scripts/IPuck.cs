using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPuck
{
    void Init(); //initializes any component fields normally set in awake, such as rigidbodies
    void Punch(); //used to initialize puck movement
    
}
