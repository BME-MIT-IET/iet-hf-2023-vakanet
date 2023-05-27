package effect;

import base.Id;

/**
 * An effect class representing Death.
 */
public class Death extends Effect {
    /**
     * Unique id.
     */
    private final static Id id = new Id();

    /**
     * Constructor, sets the name of the effect.
     */
    public Death() {
        super("Death");
    }

    @Override
    public Id getId() {
        return id;
    }
}
