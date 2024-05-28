using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContorller : MonoBehaviour
{
    PlayerCharacter character;
    private void Start()
    {
        character = GetComponent<PlayerCharacter>();
    }

    private void Update()
    {
        character.PlayerToMove(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        character.PosLimit();
        character.BodyCharacter();
        if (Input.GetMouseButtonDown(0))
        {
            character.stretching = true;
        }
        character.Pull();
        character.PickUp();
        if (Input.GetMouseButtonDown(0))
        {
            character.PutDown();
        }
    }
}
