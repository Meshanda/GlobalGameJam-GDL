using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerAnimation : GenericSingleton<PlayerAnimation>
{
    [SerializeField] private PlayableDirector _animEnterWallJumpRoom;
    [SerializeField] private PlayableDirector _animExitDashRoom;

    public void PlayEnterWallJumpAnimation()
    {
        _animEnterWallJumpRoom.Play();
    }

    public void PlayExitDashAnimation()
    {
        _animExitDashRoom.Play();
    }
}
