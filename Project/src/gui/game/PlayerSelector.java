package gui.game;

import city.Field;
import virologist.Virologist;

import javax.swing.*;
import java.awt.*;


public class PlayerSelector extends Selector<Virologist> {
    Field currentField;
    Virologist currentVirologist;

    public PlayerSelector(JFrame parent, Field currentField, Virologist virologist, String selectionReason) {
        super(parent, selectionReason);

        this.currentField = currentField;
        this.currentVirologist = virologist;

        for (Virologist v : currentField.getCharacters()) {
            if (!v.equals(virologist)) {
                comboBox.addItem(v);
                textArea.append(v + "\n");
            }

        }
        if (comboBox.getSelectedItem() != null) {
            comboBox.setRenderer(new DefaultListCellRenderer() {

                @Override
                public Component getListCellRendererComponent(JList<?> list,
                                                              Object value,
                                                              int index,
                                                              boolean isSelected,
                                                              boolean cellHasFocus) {
                    value = ((Virologist) value).getCharacterName();
                    return super.getListCellRendererComponent(list, value, index, isSelected, cellHasFocus);
                }
            });
        }
        setLocationRelativeTo(null);

    }

    public Virologist getPlayerSelected() {
        return (Virologist) comboBox.getSelectedItem();
    }

}
