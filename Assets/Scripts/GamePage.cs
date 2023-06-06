using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePage : MonoBehaviour
{
  public void ChangeWeapon()
  {
    PlayerThrowAttack playerThrowAttack = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerThrowAttack>();
    playerThrowAttack.ChangeWeapon();
  }
}
