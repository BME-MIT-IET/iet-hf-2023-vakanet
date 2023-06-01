package agent;

import base.Id;
import base.Item;
import base.TimerCallback;
import effect.Effect;
import virologist.ItemCollection;
import virologist.Virologist;

/**
 * An abstract class representing an Agent (either a virus or a vaccine). An agent instance
 * decays after a certain time.
 */
public abstract class Agent extends Item {
    /**
     * The effect which gets applied to a Virologist in case of a successful attack.
     */
    protected final Class<? extends Effect> effect;

    /**
     * The name of the corresponding effect.
     */
    protected final String effectName;

    /**
     * Id of the corresponding effect.
     */
    private final Id effectId;

    /**
     * Unique id of the object.
     */
    private final Id id = new Id();

    /**
     * Whether the Agent can be repelled or avoided.
     */
    boolean repellable = true;

    /**
     * The collection which the Agent is stored in. When the Agent decomposes it can delete itself
     * from its container.
     */
    ItemCollection<Agent> collection;

    /**
     * A timer to decompose itself after a certain time.
     */
    private final TimerCallback timerCallback = new TimerCallback(100000, this::decompose);

    Agent(Class<? extends Effect> effect) {
        this.effect = effect;
        var instance = Effect.getInstance(effect);
        effectName = instance.getName();
        effectId = instance.getId();
    }

    /**
     * Remove itself from the container.
     */
    protected void decompose() {
        System.out.println(this + " decomposed");
        try {
            if (collection != null) {
                collection.remove(this);
            }
        } catch (IllegalArgumentException ignored) {
            // agent was already used
        }
    }

    @Override
    public abstract String toString();

    /**
     * Apply the agent to the virologist.
     *
     * @param target the virologist to be used on
     */
    public abstract void applyAgent(Virologist target);

    public Id getId() {
        return id;
    }

    public Id getEffectId() {
        return effectId;
    }

    public boolean isRepellable() {
        return repellable;
    }

    public long getRemainingTime() {
        var t = timerCallback.getRemaining();
        return t > 0 ? t : 0;
    }

    /**
     * Makes the effect unrepellable.
     */
    public void setUnRepellable() {
        repellable = false;
    }

    @Override
    public void onPickUp(Virologist virologist) {
        virologist.getInventory().addAgent(this);
    }

    @Override
    public void onDrop(Virologist virologist) {
        virologist.getInventory().removeAgent(this);
    }
}
