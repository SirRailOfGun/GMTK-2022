using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public bool paused = false;

    public float roundTime = 10;
    public float turnTimer;

    public bool playerTurn = true;

    public GameObject player;
    public GameObject enemy;
    public CombatTextReadout combatLog;

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
                    turnTimer = roundTime;
                    if (playerTurn) {
                        Debug.Log("player attacks");
                        CombatRound(player.GetComponent<EquipmentAndStats>(), enemy.GetComponent<EquipmentAndStats>());
                    }
                    else
                    {
                        Debug.Log("enemy attacks");
                        CombatRound(enemy.GetComponent<EquipmentAndStats>(), player.GetComponent<EquipmentAndStats>());
                    }
                    playerTurn = !playerTurn;
                }
            }
        }
    }

    public void ResetEncounter() {
        playerTurn = true;
        turnTimer = roundTime;
        enemy = enemyGen.GenerateEnemy(currentLevel);
        enemy.transform.position = new Vector3(7,7,0);
        currentLevel++;
    }

    public void CombatRound(EquipmentAndStats attacker, EquipmentAndStats defender) {
        // Roll Weapon
        int cycleDice = attacker.GetCyclingDice();
        int cycleRoll = DiceRoller.RollDice(cycleDice);
        attacker.AdvanceWeapon(cycleRoll);

        //Debug.Log("attacker dice\ncycle " + attacker.GetCyclingDice() + "\nhit loc " + attacker.GetHitLocDice() + "\nto hit " + attacker.GetToHitDice() + "\ndamage " + attacker.GetDamageDice());

        // Roll for hit location
        int critDice = attacker.GetHitLocDice();
        int hitLocation = DiceRoller.RollDice(critDice);
        // Roll for to hit
        int hitDice = attacker.GetToHitDice();
        int toHit = DiceRoller.RollDice(hitDice);
        // Roll for parry
        int parryDice = defender.GetParryDice();
        int parry = DiceRoller.RollDice(parryDice);
        // Roll for damage
        int damageDice = attacker.GetDamageDice();
        int damage = DiceRoller.RollDice(damageDice);
        // Roll for Damage
        int armorDice = defender.GetSlotDRDice(hitLocation);
        int dr = DiceRoller.RollDice(armorDice);

        //Debug.Log("cycle by " + cyleRoll + "\nhit location " + hitLocation + "\nto hit roll " + toHit + "\nparry roll " + parry+ "\ndamage roll " + damage+ "\ndamage reduction " + dr);

        string aName = attacker.characterName;
        string dName = defender.characterName;
        string readoutText = aName + " Attacks " + dName + "\n__________________________________________" +
            "\nCycling Weapon with " + cycleDice + " dice : " + cycleRoll +
            "\n" + aName + " draws " + attacker.equipment[attacker.currentWeapon].equippedItem.name +
            "\n\nAttacking with " + critDice + " critical dice:" + (hitLocation + 1);
        string extraExc = "";
        if (hitLocation > 23)
        {
            for (int i = 0; i < hitLocation / 24; i++)
            {
                extraExc += "!";
            }
            readoutText += "\nOverctitical hit";
        }
        readoutText += "\nAttack is aimed at the " + defender.equipment[hitLocation].Name + extraExc +
            "\n\nAttack Roll with " + hitDice + " dice: " + toHit +
            "\nopponent parries with " + parryDice + " dice : " + parry;
        if (toHit < parry) {
            //Debug.Log("Attack Blocked");
            readoutText += "\nAttack is parried";
        }
        else if (damage <= dr) {
            Debug.Log("Dealt No Damage");
            readoutText += "\nAttack hits True" +
                "\n\nAttacking with " + damageDice + " damage dice : " + damage +
                "\nDefending with " + armorDice + " damage reduction dice: " + dr +
                "\nArmor deflects the blow";
        }
        else {
            Debug.Log("Hit for: " + (damage - dr));
            defender.TakeDamage(damage - dr, hitLocation);
            readoutText += "\nAttack hits True" +
                "\n\nAttacking with " + damageDice + " damage dice : " + damage +
                "\nDefending with " + armorDice + " damage reduction dice: " + dr;
            readoutText += "\nPierces Armor" +
                "\n\nAttack dealt " + (damage - dr) + " damage to the " + defender.equipment[hitLocation].Name +
                "\n" + dName + "'s " + defender.equipment[hitLocation].Name + " has " + defender.equipment[hitLocation].health + " HP remaining";
            if (defender.equipment[hitLocation].health <= 0)
            {
                readoutText += "\n\n" + dName + " is dead!";
            }
        }

        combatLog.updateLog(readoutText);
    }
}
