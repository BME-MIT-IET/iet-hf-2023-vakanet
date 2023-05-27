package effect;

import base.Id;
import virologist.Virologist;

public class BearDance extends Effect {
    static final Id id = new Id();

    /**
     * Constructor, sets the name of the effect.
     */
    public BearDance() {
        super("Bear Dance");
    }

    @Override
    public void applyEffect(Virologist v) {
        v.addEffect(this);
    }

    @Override
    public Id getId() {
        return id;
    }
}
