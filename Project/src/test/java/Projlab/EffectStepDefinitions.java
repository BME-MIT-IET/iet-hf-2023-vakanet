

package Projlab;


import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;

import java.lang.reflect.InvocationTargetException;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertTrue;


public class EffectStepDefinitions {


    String answer = "";
    Object object = null;
    Class<?> clazz = null;


    @Given("create effect {string}")
    public void create_effect(String objectName) throws ClassNotFoundException, NoSuchMethodException, InvocationTargetException, InstantiationException, IllegalAccessException {
        clazz = Class.forName("effect."+objectName);
        object = clazz.getDeclaredConstructor().newInstance();

        answer = object.getClass().getSimpleName() + " objektum sikeresen létrehozva.";
    }

    @Then("I should be told {string}")
    public void i_should_be_told(String string) {

        assertTrue(clazz.isInstance(object));
        assertEquals(string.trim().replace("\\n", "\n"), answer.trim());
    }


}
