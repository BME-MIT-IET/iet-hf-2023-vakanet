package effect;

import base.Id;
import virologist.Virologist;

/**
 * Effect that causes the Virologist to be paralyzed for a period of time.
 */
public class Paralysis extends UpdatableEffect {
    /**
     * Unique id.
     */
    private final static Id id = new Id();
    private Stealable e;

    public Paralysis() {
        super("Paralysis");
    }

    @Override
    void apply(Virologist v) {
        e = new Stealable();
        e.applyEffect(v);
    }

    @Override
    void remove(Virologist v) {
        e.removeEffect(v);
    }

    @Override
    public Id getId() {
        return id;
    }
}
