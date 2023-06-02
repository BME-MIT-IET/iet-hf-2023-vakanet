package equipment;

import base.GameException;
import effect.Death;
import gui.Window;
import gui.game.GameScene;
import virologist.Virologist;

/**
 * An equipment class representing an axe.
 */
public class Axe extends Equipment {
    /**
     * The axe can only be used once, this marks whether it has been used.
     */
    boolean used = false;

    /**
     * Use the axe on a virologist, if the axe is still usable.
     *
     * @param virologist the virologist to use the axe on
     */
    public void useAxeOn(Virologist virologist) {
        if (!used) {
            used = true;
            var d = new Death();
            d.applyEffect(virologist);
            System.out.println("Axe used on " + virologist);
            GameScene gs = Window.getGameScene();
            if (gs != null)
                gs.logAction("Axe used on " + virologist);
        } else {
            throw new GameException("Axe was used already");
        }
    }

    @Override
    void applyEffect(Virologist v) {
        // Empty override on purpose
    }

    @Override
    void removeEffect(Virologist v) {
        // Empty override on purpose
    }

    @Override
    public String toString() {
        return "Axe#" + id.getId();
    }
}
