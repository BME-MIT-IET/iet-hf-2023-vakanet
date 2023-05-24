package effect;

import base.Id;

/**
 * Effect that provides immunity against an effect for a period of time.
 */
public class Immunity extends UpdatableEffect {
    /**
     * Unique id.
     */
    private final static Id id = new Id();

    /**
     * The Immunity is against this Effect.
     */
    private final Class<? extends Effect> effect;

    /**
     * The Id of the effect which the immunity is against.
     */
    private final Id effectId;

    public Immunity(Class<? extends Effect> effect) {
        super("");
        this.effect = effect;
        var instance = Effect.getInstance(effect);
        this.name = instance.name + " immunity";
        effectId = instance.getId().getNegative();
    }

    @Override
    public Id getId() {
        return effectId;
    }
}
