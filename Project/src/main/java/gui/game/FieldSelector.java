package gui.game;

import agent.Agent;
import city.Field;

import javax.swing.*;
import java.awt.*;

public class FieldSelector extends Selector<Field> {

    JComboBox<Agent> agentComboBox;
    Field currentField;

    public FieldSelector(JFrame parent, Field currentField) {
        super(parent, "Select Field to move to:");
        this.currentField = currentField;

        for (Field field : currentField.getNeighbours()) {
            comboBox.addItem(field);
            textArea.append(field + "\n");

        }
        comboBox.setRenderer(new DefaultListCellRenderer() {

            @Override
            public Component getListCellRendererComponent(JList<?> list,
                                                          Object value,
                                                          int index,
                                                          boolean isSelected,
                                                          boolean cellHasFocus) {
                return super.getListCellRendererComponent(list, value, index, isSelected, cellHasFocus);
            }
        });
        setLocationRelativeTo(null);
    }

    public Field getField() {
        return (Field) comboBox.getSelectedItem();
    }

}
