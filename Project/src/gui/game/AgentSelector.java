package gui.game;

import agent.Agent;
import virologist.Virologist;

import javax.swing.*;
import java.awt.*;

public class AgentSelector extends Selector<Agent> {

    Virologist currentVirologist;

    public AgentSelector(JFrame parent, Virologist currentVirologist) {
        super(parent, "Select Agent to use:");

        this.currentVirologist = currentVirologist;
        var agents = currentVirologist.getInventory().getAgents();
        for (Agent agent : agents) {
            comboBox.addItem(agent);
            textArea.append(agent + "\n");

        }
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
        setLocationRelativeTo(null);
    }

    public Agent getAgentSelected() {
        return (Agent) comboBox.getSelectedItem();
    }
}
