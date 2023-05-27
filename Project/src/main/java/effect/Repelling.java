package effect;

import base.Id;

/**
 * If a Virologist wearing this effect gets attacked with a repellable Agent
 * the attack gets reversed.
 */
public class Repelling extends Effect {
    /**
     * Unique id.
     */
    private final static Id id = new Id();

    public Repelling() {
        super("Repelling");
    }

    @Override
    public Id getId() {
        return id;
    }
}
