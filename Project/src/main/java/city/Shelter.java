package city;

import equipment.Equipment;
import gui.Window;
import gui.game.GameScene;
import virologist.Virologist;

/**
 * A field modeling a shelter. In a shelter there might be an equipment which can be picked up.
 */
public class Shelter extends Field {
    /**
     * The equipment in the shelter, it disappears when picked up.
     */
    Equipment equipment;

    /**
     * Constructor, set the equipment.
     *
     * @param equipment the equipment in the shelter
     */
    public Shelter(Equipment equipment) {
        super();
        this.equipment = equipment;
    }

    /**
     * Add the equipment to the inventory of the virologist.
     *
     * @param v the virologist to give the item to
     */
    @Override
    public void addItemTo(Virologist v) {
        if (equipment != null) {
            GameScene gs = Window.getGameScene();
            if (gs != null)
                gs.logAction(equipment + " was successfully equipped by " + v.getCharacterName());
            equipment.onPickUp(v);
            equipment = null; // clear
        }
    }

    @Override
    public String toString() {
        return "Shelter#" + id.getId();
    }
}
