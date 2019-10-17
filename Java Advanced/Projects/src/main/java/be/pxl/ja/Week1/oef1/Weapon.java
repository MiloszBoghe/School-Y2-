package be.pxl.ja.Week1.oef1;

public abstract class Weapon {
    public final static double MODIFIER_CHANGE_RATE = 0;
    private String name;
    private double weight;
    private final double baseDamage = 0;
    private double damageModifier = 0;
    private final double baseDurability = 0;
    private double durabilityModifier = 0;




    public abstract void polish();

    public String getName() {
        return name;
    }

    public boolean isBroken() {
        return getDurability() == 0;
    }

    public double getBaseDamage() {
        return baseDamage;
    }

    public double getDamage() {
        return baseDamage + damageModifier;
    }

    public double getDamageModifier() {
        return damageModifier;
    }

    public void setDamageModifier(double damageModifier) {
        this.damageModifier = damageModifier;
    }

    public double getBaseDurability() {
        return baseDurability;
    }

    public double getDurability() {
        double durability = baseDurability + durabilityModifier;
        return durability <= 0 ? 0 : Math.min(durability, 1.0);
    }

    public void setDurabilityModifier(double durabilityModifier) {
        this.durabilityModifier = durabilityModifier;
    }

    public double getDurabilityModifier() {
        return durabilityModifier;
    }

    public double use() {
        if (isBroken()) {
            return 0.0;
        } else {
            durabilityModifier -= MODIFIER_CHANGE_RATE;
            return getDamage();
        }
    }

    @Override
    public String toString() {
        return String.format("%s -- Weight: %.2f, Damage: %.2f, Durability: %.2f%%", name, weight, getDamage(), getDurability());
    }
}
