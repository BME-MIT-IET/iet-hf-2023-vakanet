package Projlab;


import effect.Amnesia;
import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;

import java.util.Arrays;

import static org.junit.Assert.assertEquals;


public class StepDefinitions {

    private String[] allowedClasses = { "Amnesia" };
    String answer = "";

    @Given("create object {string}")
    public void create_object(String objectName) {
        if (Arrays.asList(allowedClasses).contains(objectName)) {
            answer = objectName + " objektum sikeresen létrehozva.";

            switch (objectName) {
                case "Amnesia":
                    new Amnesia();
                    break;


                    //..
                default:
                    return;
            }
        }
    }

    @Then("I should be told {string}")
    public void i_should_be_told(String string) {

        assertEquals(string.trim().replace("\\n", "\n"), answer.trim());
    }


}
