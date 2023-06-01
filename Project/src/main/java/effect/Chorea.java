package effect;

import base.Id;

/**
 * An effect that causes the Virologist to move uncontrollably for a period of time.
 */
public class Chorea extends UpdatableEffect {
    /**
     * Unique id.
     */
    private final static Id id = new Id();

    public Chorea() {
        super("Chorea");
    }

    @Override
    public Id getId() {
        return id;
    }
}
