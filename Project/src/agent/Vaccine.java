package agent;

import base.Id;
import effect.Effect;
import effect.Immunity;
import virologist.Virologist;

/**
 * An Agent representing a vaccine. The virus is a defensive Agent, it is not repellable by default.
 */
public class Vaccine extends Agent {
    public Vaccine(Class<? extends Effect> effect) {
        super(effect);
        repellable = false;
        System.out.println(this + " created");
    }

    @Override
    public String toString() {
        return effectName + " vaccine#" + getId().getId() + " (" + getRemainingTime() / 1000 + " s)";
    }

    /**
     * Apply an immunity on a virologist.
     *
     * @param target the virologist to be used on
     */
    @Override
    public void applyAgent(Virologist target) {
        final var e = new Immunity(effect);
        e.applyEffect(target);
    }

    @Override
    public Id getEffectId() {
        return super.getEffectId().getNegative();
    }
}
