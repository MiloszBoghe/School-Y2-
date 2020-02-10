package be.pxl.ja.Week1.oef1;

public class Sword extends Weapon {

    public Sword(double weight, double damageModifier, double durabilityModifier) {
        //super("sword", weight, damageModifier, durabilityModifier);
    }

    @Override
    public void polish() {
        if ((getDamageModifier() + MODIFIER_CHANGE_RATE) > (0.25 * getBaseDamage())) {
            setDamageModifier(0.25 * getBaseDamage());
        }
    }
}