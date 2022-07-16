using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public bool paused = false;

    public float roundTime = 2;
    public float turnTimer;

    public bool playerTurn = true;

    public EquipmentAndStats player;
    public EquipmentAndStats enemy;

    public int currentLevel;
    private EnemyGenerator enemyGen;

    // Start is called before the first frame update
    void Start()
    {
        enemyGen = GetComponent<EnemyGenerator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!paused) {
            if (player == null || enemy == null) {
                paused = true;
                if (player == null) {
                    Debug.Log("Game Over");
                }
                else {
                    Debug.Log("Victory");
                    ResetEncounter();
                }
            }
            else {
                turnTimer -= Time.deltaTime;
                if (turnTimer < 0) {
                    turnTimer = 5;
                    if (playerTurn) {
                        CombatRound(player, enemy);
                    }
                    else {
                        CombatRound(enemy, player);
                    }
                    playerTurn = !playerTurn;
                }
            }
        }
    }

    public void ResetEncounter() {
        playerTurn = true;
        turnTimer = roundTime;
        enemyGen.GenerateEnemy(currentLevel);
        currentLevel++;
    }

    public void CombatRound(EquipmentAndStats attacker, EquipmentAndStats defender) {
        // Roll Weapon
        int cyleRoll = DiceRoller.RollDice(attacker.GetCyclingDice());
        attacker.AdvanceWeapon(cyleRoll);
        // Roll for hit location
        int hitLocation = DiceRoller.RollDice(attacker.GetHitLocDice());
        // Roll for to hit
        int toHit = DiceRoller.RollDice(attacker.GetToHitDice());
        // Roll for parry
        int parry = DiceRoller.RollDice(defender.GetParryDice());
        // Roll for damage
        int damage = DiceRoller.RollDice(attacker.GetDamageDice());
        // Roll for Damage
        int dr = DiceRoller.RollDice(defender.GetSlotDRDice(hitLocation));

        if (toHit < parry) {
            Debug.Log("Attack Blocked");
        }
        else if (damage <= dr) {
            Debug.Log("Dealt No Damage");
        }
        else {
            Debug.Log("Hit for: " + (damage - dr));
            defender.TakeDamage(damage - dr, hitLocation);
        }
    }
}
