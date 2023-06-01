package effect;

import base.Id;

/**
 * Effect that provides immunity against an effect for a period of time.
 */
public class Immunity extends UpdatableEffect {

    /**
     * The Immunity is against this Effect.
     */
    private final Class<? extends Effect> effect;

    /**
     * The Id of the effect which the immunity is against.
     */
    private final Id id;

    public Immunity(Class<? extends Effect> effect) {
        super("");
        this.effect = effect;
        var instance = Effect.getInstance(effect);
        this.name = instance.name + " immunity";
        id = instance.getId().getNegative();
    }

    @Override
    public Id getId() {
        return id;
    }
}
