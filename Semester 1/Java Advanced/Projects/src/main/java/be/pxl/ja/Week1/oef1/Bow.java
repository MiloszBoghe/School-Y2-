package be.pxl.ja.Week1.oef1;

public class Bow extends Weapon {

    public Bow(double weight, double damageModifier, double durabilityModifier) {
        //super("bow", weight, damageModifier, durabilityModifier);
    }

    public Bow() {

    }

    @Override
    public void polish() {
        if ((getDurability() + MODIFIER_CHANGE_RATE) > 1) {
            setDurabilityModifier(1);
        }
    }
}
