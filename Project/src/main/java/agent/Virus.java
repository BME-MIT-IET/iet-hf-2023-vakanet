package agent;

import effect.Effect;
import virologist.Virologist;


/**
 * An Agent representing a virus. The virus is an offensive Agent.
 */
public class Virus extends Agent {
    public Virus(Class<? extends Effect> effect) {
        super(effect);
        System.out.println(this + " created");
    }

    @Override
    public String toString() {
        return effectName + " virus#" + getId().getId() + " (" + getRemainingTime() / 1000 + " s)";
    }

    /**
     * Apply the effect to a virologist.
     *
     * @param target the virologist to be used on
     */
    @Override
    public void applyAgent(Virologist target) {
        final var e = Effect.getInstance(effect);
        e.applyEffect(target);
    }
}
