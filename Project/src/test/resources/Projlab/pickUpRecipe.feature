Feature: pickUpRecipe
  Scenario: pickUpParalysis
    Given create laboratory "lab1" with "Paralysis"
    Given create virologist "Virologist" on field "lab1"
    When collect "Virologist"

    Then "Virologist" has recipe "Paralysis"

  Scenario: pickUpAmnesia
    Given create laboratory "lab2" with "Amnesia"
    Given create virologist "Virologist" on field "lab2"
    When collect "Virologist"

    Then "Virologist" has recipe "Amnesia"

  Scenario: pickUpImmunity
    Given create laboratory "lab3" with "Immunity"
    Given create virologist "Virologist" on field "lab3"
    When collect "Virologist"

    Then "Virologist" has recipe "Immunity"

  Scenario: pickUpBearDance
    Given create laboratory "lab4" with "BearDance"
    Given create virologist "Virologist" on field "lab4"
    When collect "Virologist"

    Then "Virologist" has recipe "BearDance"
