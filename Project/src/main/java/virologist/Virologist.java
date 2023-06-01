package virologist;

import agent.Agent;
import agent.Recipe;
import base.*;
import city.Field;
import effect.*;
import equipment.Axe;
import equipment.Cloak;
import equipment.Gloves;
import gui.Window;
import gui.game.GameScene;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Random;
import java.util.stream.Collectors;

/**
 * Class representing an in-game player.
 */
public class Virologist implements Updatable, Serializable {
    /**
     * The list of Effects currently applied to the Virologist.
     */
    private final List<Effect> appliedEffects = new ArrayList<>();

    /**
     * The name of the Virologist.
     */
    private final String characterName;

    /**
     * The inventory of the Virologist.
     */
    private final Inventory inventory = new Inventory();


    private final Random random = new Random();
    /**
     * Unique id of the virologist.
     */
    private final Id id = new Id();

    /**
     * The field which the virologist is currently on.
     */
    private Field field;

    public Virologist(Field f, String name) {
        Timer.getInstance().add(this);
        characterName = name;
        field = f;
        this.addEffect(new Stealable());
    }

    public Inventory getInventory() {
        return inventory;
    }

    /**
     * Get list of items which can be stolen. If the virologist is not paralyzed or
     * the virologist was already stolen from, the list is empty.
     *
     * @return the list of stealable items
     */
    public List<Item> getStealable() {
        final List<Item> stealable = new ArrayList<>();

        if (isStealable()) {
            stealable.addAll(getInventory().equipments.getView());
            stealable.addAll(getInventory().ingredients.getView());
        }

        return stealable;
    }

    /**
     * A function to call when the virologist tries to do something. If the virologist
     * is allowed to take actions nothing happens, if something prevents the virologist
     * from taking action, a GameException is thrown.
     * <p>
     * Needs to be called before:
     * - moving
     * - crafting
     * - picking up something
     * - interacting with other players
     * <p>
     * Doesn't need to be called before reading state, listing items etc.
     */
    public void tryStartAction() {
        if (appliedEffects.contains(new Death())) throw new GameException(this + " is dead");
        if (appliedEffects.contains(new Paralysis())) throw new GameException(this + " is paralyzed");
        if (appliedEffects.contains(new Chorea())) throw new GameException(this + " has Chorea");
        if (appliedEffects.contains(new BearDance())) throw new GameException(this + " has Bear Virus");
    }

    /**
     * Use Axe on target.
     *
     * @param vid the virologist to use the axe on
     */
    public void useAxe(Id vid) {
        tryStartAction();

        Axe axe = (Axe) inventory.equipments.getView()
                .stream()
                .filter(x -> x.getClass().equals(Axe.class))
                .findFirst()
                .orElseThrow(() -> new GameException(this + " doesn't have an axe"));

        final var target = field.getCharacters()
                .stream()
                .filter(x -> x.getId().equals(vid))
                .findFirst()
                .orElseThrow(() -> new GameException("No such Virologist"));

        axe.useAxeOn(target);
    }

    /**
     * The Virologist is attacked by another Virologist with an Agent.
     *
     * @param v     the attacker
     * @param agent the agent
     */
    public void attackedBy(Virologist v, Agent agent) {
        final boolean repellable = agent.isRepellable();
        final boolean isImmune = appliedEffects.stream()
                .anyMatch(e -> e.getId().equalsNegative(agent.getEffectId()));

        GameScene gs = Window.getGameScene();
        if (gs != null)
            gs.logAction(v.getCharacterName() + " is attacking " + v.getCharacterName() + " with" + agent);
        if (isImmune) {
            System.out.println(this + " is immune to " + agent);
            if (gs != null)
                gs.logAction(this + " is immune to " + agent);
            return;
        }


        if (repellable) {
            boolean hasResistance = appliedEffects.contains(new Resistance());
            if (hasResistance && !Cloak.getAttackSuccess()) {
                System.out.println(this + " was protected by Cloak");
                if (gs != null)
                    gs.logAction(this + " was protected by Cloak");
                return;
            }

            boolean hasGloves = appliedEffects.contains(new Repelling());
            if (hasGloves) {
                Gloves gloves = (Gloves) inventory.equipments.getView()
                        .stream()
                        .filter(x -> x.getClass().equals(Gloves.class))
                        .findFirst()
                        .orElseThrow(() -> new RuntimeException("Something went very wrong, " + this + " should have an Axe"));

                if (gloves.use()) {
                    System.out.println(this + " reversed the attack with gloves");
                    if (gs != null)
                        gs.logAction(this + " reversed the attack with gloves");
                    agent.setUnRepellable();
                    v.attackedBy(this, agent);
                    return;
                }
            }
        }

        agent.applyAgent(this);
        System.out.println(agent + " applied to " + this);
        if (gs != null)
            gs.logAction(agent + " applied to " + this);
    }

    public String getCharacterName() {
        return characterName;
    }

    public Field getField() {
        return field;
    }

    /**
     * Set the current field of the virologist.
     *
     * @param field the field of the virologist
     */
    public void setField(Field field) {
        this.field = field;
    }

    /**
     * Try to move to a field.
     *
     * @param f the field to move to
     */
    public void move(Field f) {
        tryStartAction();
        field.move(this, f);
    }

    /**
     * Try to collect items from the current field.
     */
    public void collect() {
        tryStartAction();
        field.addItemTo(this);
    }

    /**
     * Try to craft an agent using a recipe.
     *
     * @param vaccine  true for vaccine false for virus
     * @param recipeId id of the recipe to use
     */
    public void craft(boolean vaccine, Id recipeId) {
        tryStartAction();
        final var recipes = inventory.recipes.getView()
                .stream()
                .collect(Collectors.toMap(Recipe::getId, r -> r));

        if (!recipes.containsKey(recipeId)) throw new GameException("No such recipe");
        final var recipe = recipes.get(recipeId);

        if (vaccine) {
            inventory.addAgent(recipe.createVaccine(inventory.ingredients));
        } else {
            inventory.addAgent(recipe.createVirus(inventory.ingredients));
        }
    }

    boolean isStealable() {
        return appliedEffects.stream().anyMatch(e -> e.getId().equals((new Stealable()).getId()));
    }

    void trySteal() {
        if (!isStealable()) throw new GameException(this + " can't be stolen from");
        appliedEffects.stream()
                .filter(e -> e.getId().equals((new Stealable()).getId()))
                .findFirst()
                .ifPresent(e -> e.removeEffect(this));
    }

    /**
     * Try to steal an item from a virologist.
     *
     * @param item   the item to steal
     * @param target the virologist to steal from
     */
    public void steal(Item item, Virologist target) {
        tryStartAction();
        target.trySteal();

        // pick up
        item.onPickUp(this);

        // make the target drop the item
        item.onDrop(target);
        GameScene gs = Window.getGameScene();
        if (gs != null)
            gs.logAction(item + " was stolen from " + target.getCharacterName() + " by " + this.getCharacterName());
    }

    /**
     * Try to use an agent on a virologist.
     *
     * @param aid agent to use
     * @param vid virologist to use agent on
     */
    public void use(Id aid, Id vid) {
        tryStartAction();

        final var agent = getInventory()
                .getAgents()
                .stream()
                .filter(a -> a.getId().equals(aid))
                .findFirst()
                .orElseThrow(() -> new GameException("No such agent"));

        final var target = field.getCharacters()
                .stream()
                .filter(x -> x.getId().equals(vid))
                .findFirst()
                .orElseThrow(() -> new GameException("No such virologist"));

        getInventory().removeAgent(agent);
        target.attackedBy(this, agent);
    }

    /**
     * Get the list of applied effects.
     *
     * @return the list of applied effects
     */
    public List<Effect> getAppliedEffects() {
        return Collections.unmodifiableList(appliedEffects);
    }

    /**
     * Move to a random available field. If there are no neighboring fields, nothing happens.
     */
    void moveRandom() {
        var available = field.getNeighbours();
        if (available.size() == 0) return;

        var f = available.get(random.nextInt(available.size()));
        field.move(this, f);
    }

    @Override
    public void update() {
        if (appliedEffects.contains(new Death())) return;

        var chorea = appliedEffects.contains(new Chorea());
        var bear = appliedEffects.contains(new BearDance());

        if (chorea || bear) moveRandom();

        if (bear) {
            field.getCharacters().forEach(v -> {
                if (v == this) return;

                // handle cloak
                if (!(v.getAppliedEffects().contains(new Resistance()) && Cloak.getAttackSuccess())) {
                    // add virus to virologist
                    v.addEffect(new BearDance());
                }
            });
        }
    }

    @Override
    public String toString() {
        return characterName + "#" + id.getId();
    }

    public Id getId() {
        return id;
    }

    /**
     * Add an effect to the virologist.
     * If the effect is an immunity, the virologist get cured.
     * If the effect is a virus, and the virologist has an immunity, the infection is prevented.
     *
     * @param e the effect to add
     */
    public synchronized void addEffect(Effect e) {
        if (e.getId().getId() < 0) {
            // effect is Immunity
            appliedEffects.stream()
                    .filter(v -> v.getId().equalsNegative(e.getId()))
                    .collect(Collectors.toList()) // collect before removing
                    .forEach(this::removeEffect);
        } else {
            if (appliedEffects.stream().anyMatch(i -> i.getId().equalsNegative(e.getId()))) {
                System.out.println(this + " is immune to " + e);
                return;
            }
        }
        appliedEffects.add(e);
        e.setOwner(this);
        String message = "Applied " + e + " to " + this;
        System.out.println(message);
        GameScene gs = Window.getGameScene();
        if (gs != null && e.getClass() != Stealable.class)
            gs.logAction(message);
    }

    public synchronized void removeEffect(Effect e) {
        appliedEffects.remove(e);
        String message = "Removed " + e + " from " + this;
        System.out.println(message);
        GameScene gs = Window.getGameScene();
        if (gs != null && e.getClass() != Stealable.class)
            gs.logAction(message);
    }
}