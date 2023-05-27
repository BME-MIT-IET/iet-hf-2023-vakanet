package city;

import agent.Recipe;
import effect.BearDance;
import effect.Resistance;
import equipment.Cloak;
import virologist.Virologist;

/**
 * A laboratory field contaminated with a bear virus, virologists who enter
 * get infected.
 */
public class BearVirusLaboratory extends Laboratory {
    /**
     * Constructor, set the recipe of the laboratory
     *
     * @param recipe the recipe found in the laboratory
     */
    public BearVirusLaboratory(Recipe recipe) {
        super(recipe);
    }

    /**
     * The bear virus laboratories attack the virologists which enter it.
     *
     * @param virologist the virologist to add
     */
    @Override
    public void addVirologist(Virologist virologist) {
        if (!(virologist.getAppliedEffects().contains(new Resistance()) && Cloak.getAttackSuccess())) {
            // add virus to virologist
            virologist.addEffect(new BearDance());
        }

        super.addVirologist(virologist);
    }
}
