using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{

    public EnumMouvement.MouvementEnum enumMouvement;
    public EnumWeapon.WeaponEnum enumWeapon;

    public IMovementType movementType;
    public IWeaponType weaponType;
    public TeamScript team;

    public int hp {
        get;
        set;
    }
    private int atk;
    private int spd;
    private int def;
    private int res;

    private int defaultHp = 10;
    private int defaultAtk = 10;
    private int defaultSpd = 10;
    private int defaultDef = 10;
    private int defaultRes = 10;

    void Awake()
    {
        ConfigCharacter c = ScriptableObject.CreateInstance<ConfigCharacter>();
        movementType = (IMovementType) c.SetupMouvement(enumMouvement);
        weaponType = (IWeaponType)c.SetupWeapon(enumWeapon);
        this.SetupCharacter();
    }

    private void OnMouseDown()
    {
        GridScript grid = gameObject.transform.parent.parent.GetComponent<GridScript>();
        grid.textUpdater.UpdateUnitValue(this.gameObject.GetComponent<CharacterScript>());

        this.gameObject.transform.parent.GetChild(0).GetComponent<CaseScript>().OnMouseDown();
    }

    public int GetAtk()
    {
        return this.atk + weaponType.GetAtk();
    }

    public int GetHP()
    {
        return this.hp;
    }

    public int GetSpeed()
    {
        return this.spd + weaponType.GetSpd();
    }

    public int GetDefense()
    {
        return this.def;
    }

    public int GetResistance()
    {
        return this.res;
    }

    public int GetMovementNumber()
    {
        return movementType.GetMovementNumber();
    }

    public void SetPosition(Transform parent)
    {
        this.transform.SetParent(parent);
    }

    public void SetHP(int damage)
    {
        this.hp -= damage;
    }

    public void SetupCharacter()
    {
        this.hp = defaultHp + movementType.GetBonusHp();
        this.atk = defaultAtk + movementType.GetBonusAtk();
        this.spd = defaultSpd + movementType.GetBonusSpd();
        this.def = defaultDef + movementType.GetBonusDef();
        this.res = defaultRes + movementType.GetBonusRes();
    }

    public void Die()
    {
        team.characters.Remove(this.transform);
        Destroy(gameObject);
    }
}
