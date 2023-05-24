package effect;

import base.Id;
import virologist.Virologist;

import java.util.Objects;

/**
 * Abstract class to represent an effect.
 */
public abstract class Effect {
    /**
     * Name of the effect.
     */
    protected String name;

    /**
     * The virologist on which the effect is applied to. This reference is needed, so the
     * effect can remove itself.
     */
    protected Virologist owner;

    /**
     * Constructor, sets the name of the effect.
     *
     * @param name the name of the effect
     */
    Effect(String name) {
        this.name = name;
    }

    /**
     * Static method to retrieve an instance of an Effect
     *
     * @param effect effect class off the needed effect instance
     * @return the instance of the effect
     */
    public static Effect getInstance(Class<? extends Effect> effect) {
        try {
            // create an instance of the effect and return it
            return effect.getDeclaredConstructor().newInstance();
        } catch (Exception e) {
            e.printStackTrace();
            throw new RuntimeException("Something went very wrong");
        }
    }

    public String getName() {
        return name;
    }

    /**
     * Returns the string representation of the effect (it's name).
     *
     * @return the effect's name
     */
    @Override
    public String toString() {
        return name + "#" + getId().getId();
    }

    void apply(Virologist v) {
    }

    void remove(Virologist v) {
    }

    public void applyEffect(Virologist v) {
        v.addEffect(this);
        apply(v);
    }

    public void removeEffect(Virologist v) {
        v.removeEffect(this);
        remove(v);
    }

    /**
     * Returns a unique identification of the effect.
     *
     * @return the unique Id
     */
    public abstract Id getId();

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Effect effect = (Effect) o;
        return getId().equals(effect.getId());
    }

    @Override
    public int hashCode() {
        return Objects.hash(getId());
    }

    /**
     * Set the owner of the effect.
     *
     * @param owner the virologist which the effect is on
     */
    public void setOwner(Virologist owner) {
        this.owner = owner;
    }
}
