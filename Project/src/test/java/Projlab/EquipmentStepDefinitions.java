package Projlab;

import Projlab.util.ObjectMap;
import city.Shelter;
import equipment.Equipment;
import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import virologist.Virologist;

import java.lang.reflect.InvocationTargetException;

import static org.junit.Assert.assertTrue;

public class EquipmentStepDefinitions {
    ObjectMap objectMap = ObjectMap.getInstance();
    @Given("create shelter {string} with {string}")
    public void create_shelter(String warehouse, String item) throws ClassNotFoundException, NoSuchMethodException, InvocationTargetException, InstantiationException, IllegalAccessException {
        Class<?> clazz = Class.forName("equipment."+item);
        Object object = clazz.getDeclaredConstructor().newInstance();

        objectMap.addObject(item,object);

        objectMap.addObject(warehouse,new Shelter((Equipment)object));
    }

    @Then("{string} has equipment {string}")
    public void has_equipment(String string1, String string2) {
        var equipments = objectMap.<Virologist>getObject(string1).getInventory().getEquipments();
        assertTrue(equipments.contains(objectMap.<Equipment>getObject(string2)));

    }
}
