package equipment;

import effect.Repelling;
import virologist.Virologist;

/**
 * An equipment class representing gloves. The gloves apply a Repelling
 * effect on its owner.
 */
public class Gloves extends Equipment {
    /**
     * The repelling effect of the gloves.
     */
    final Repelling r = new Repelling();

    int uses = 3;

    @Override
    public String toString() {
        return "Gloves#" + id.getId();
    }

    @Override
    void applyEffect(Virologist v) {
        v.addEffect(r);
    }

    public boolean use() {
        if (uses > 0) {
            uses -= 1;

            return true;
        }

        return false;
    }

    @Override
    void removeEffect(Virologist v) {
        v.removeEffect(r);
    }
}
