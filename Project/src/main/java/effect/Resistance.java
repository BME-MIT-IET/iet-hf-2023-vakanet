package effect;

import base.Id;

/**
 * The Virologist wearing this effect has a 82.3% chance of being unharmed when attacked with
 * a repellable Agent.
 */
public class Resistance extends Effect {
    /**
     * Unique id.
     */
    private final static Id id = new Id();

    public Resistance() {
        super("Resistance");
    }

    @Override
    public Id getId() {
        return id;
    }
}
