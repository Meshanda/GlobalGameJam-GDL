using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerAnimation : GenericSingleton<PlayerAnimation>
{
    [SerializeField] private PlayableDirector _animEnterWallJumpRoom;

    public void PlayEnterWallJumpAnimation()
    {
        _animEnterWallJumpRoom.Play();
    }
}
