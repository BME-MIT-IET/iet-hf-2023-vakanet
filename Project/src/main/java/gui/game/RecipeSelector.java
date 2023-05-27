package gui.game;

import agent.Agent;
import agent.Recipe;
import base.GameException;
import virologist.Virologist;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;

public class RecipeSelector extends Selector<Recipe> {

    Virologist virologist;

    JRadioButton vaccineButton;
    JRadioButton virusButton;

    private Agent agent;

    public RecipeSelector(JFrame parent, Virologist virologist) {
        super(parent, "Select recipe to craft:");
        this.virologist = virologist;

        for (Recipe r : virologist.getInventory().getRecipes()) {
            comboBox.addItem(r);
            textArea.append(r + "\n");
        }
        if (comboBox.getSelectedItem() != null) {
            comboBox.setRenderer(new DefaultListCellRenderer() {

                @Override
                public Component getListCellRendererComponent(JList<?> list,
                                                              Object value,
                                                              int index,
                                                              boolean isSelected,
                                                              boolean cellHasFocus) {
                    value = value;
                    return super.getListCellRendererComponent(list, value, index, isSelected, cellHasFocus);
                }
            });
        }

        virusButton = new JRadioButton();
        virusButton.setText("Virus");
        vaccineButton = new JRadioButton();
        vaccineButton.setText("Vaccine");
        ButtonGroup group = new ButtonGroup();
        group.add(virusButton);
        group.add(vaccineButton);
        JPanel radioPanel = new JPanel();
        radioPanel.add(vaccineButton);
        radioPanel.add(virusButton);

        lowerPanel.add(radioPanel, 0);
        setLocationRelativeTo(null);
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        if (e.getActionCommand().equals(exitButton.getActionCommand())) {
            this.setVisible(false);
        } else if (e.getActionCommand().equals(selectButton.getActionCommand())) {
            Recipe r = (Recipe) comboBox.getSelectedItem();
            if (r == null) {
                JOptionPane.showMessageDialog(this, "You must select an agent to craft!");
                return;
            }
            try {
                r.trySubtractRequired(virologist.getInventory().getIngredients());
            } catch (GameException exception) {
                JOptionPane.showMessageDialog(this, "Not enough ingredients to craft Agent!");
                return;
            }
            if (vaccineButton.isSelected()) {
                agent = r.createVaccine(virologist.getInventory().getIngredients());
                virologist.getInventory().getAgents().add(agent);
            } else if (virusButton.isSelected()) {
                agent = r.createVirus(virologist.getInventory().getIngredients());
                virologist.getInventory().getAgents().add(agent);
            } else {
                JOptionPane.showMessageDialog(this, "First select the type of Agent you wish to create!");
                return;
            }
            this.setVisible(false);

            selectionDone = true;
        }
    }

    public Agent getAgent() {
        return agent;
    }
}
