package effect;

import base.Id;
import base.TimerCallback;
import virologist.Virologist;

/**
 * An abstract effect type which has an effect for a limited time.
 */
public abstract class UpdatableEffect extends Effect {
    /**
     * A timer to remove the effect after a certain time.
     */
    TimerCallback timerCallback = null;

    UpdatableEffect(String name) {
        super(name);
    }

    @Override
    void apply(Virologist v) {
    }

    @Override
    public void applyEffect(Virologist v) {
        // set the timer for the effect to be removed
        timerCallback = new TimerCallback(10000, () -> {
            System.out.println(this + " expired.");
            owner.removeEffect(this);
        });

        // apply the effect
        v.addEffect(this);
        apply(v);
    }

    @Override
    public Id getId() {
        return null;
    }

    @Override
    public String toString() {
        var s = super.toString();
        if (timerCallback != null) {
            s = s + " (" + timerCallback.getRemaining() / 1000 + " s)";
        }
        return s;
    }
}
