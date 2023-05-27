package effect;

import base.Id;
import virologist.Virologist;

public class Stealable extends UpdatableEffect {
    /**
     * Unique id.
     */
    private final static Id id = new Id();

    /**
     * Constructor, sets the name of the effect.
     */
    public Stealable() {
        super("Stealable");
    }

    @Override
    public void removeEffect(Virologist v) {
        v.removeEffect(this);
    }

    @Override
    public Id getId() {
        return id;
    }
}
