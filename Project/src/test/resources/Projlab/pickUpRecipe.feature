Feature: pickUpRecipe
  Scenario: pickUpRecipe
    Given create laboratory "Laboratory" with "Paralysis"
    Given create object "Virologist" on field "Laboratory"
    When collect "Virologist"

    Then "Virologist" has "Paralysis"


