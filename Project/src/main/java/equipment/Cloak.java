package equipment;

import effect.Resistance;
import virologist.Virologist;

import java.util.Random;

/**
 * Equipment class representing a cloak. The cloak applies a Resistance effect
 * to its owner, it can protect its owner from attacks.
 */
public class Cloak extends Equipment {
    /**
     * The state of all cloaks in the game.
     */
    private static State state = State.RANDOM;
    /**
     * The effect of the Cloak.
     */
    private final Resistance r = new Resistance();

    private static Random random = new Random();
    /**
     * Set the state of all cloaks.
     *
     * @param s the state
     */
    public static void setState(State s) {
        state = s;
    }

    /**
     * Returns whether an attack is successful if its owner is attacked
     *
     * @return true if the attack is successful, false if the cloak protects its owner
     */
    public static boolean getAttackSuccess() {
        boolean success = false;
        switch (state) {
            case ON:
                success = false;
                break;
            case OFF:
                success = true;
                break;
            case RANDOM:
                success = random.nextDouble() <= 0.823;
                break;
        }
        return success;
    }

    @Override
    public String toString() {
        return "Cloak#" + id.getId();
    }

    @Override
    void applyEffect(Virologist v) {
        v.addEffect(r);
    }

    @Override
    void removeEffect(Virologist v) {
        v.removeEffect(r);
    }

    /**
     * Available configurations of the Cloak
     * <p>
     * OFF: the cloak doesn't protect its owner
     * ON: the cloak protects its owner
     * RANDOM: the cloak protects its owner with a certain probability
     */
    public enum State {
        OFF, ON, RANDOM
    }
}
