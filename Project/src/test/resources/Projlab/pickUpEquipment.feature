Feature: pickUpItem
  Scenario: pickUpGloves
    Given create shelter "sh" with "Gloves"
    Given create virologist "Virologist" on field "sh"
    When collect "Virologist"
    Then "Virologist" has equipment "Gloves"

  Scenario: pickUpBag
    Given create shelter "sh" with "Bag"
    Given create virologist "Virologist" on field "sh"
    When collect "Virologist"
    Then "Virologist" has equipment "Bag"

  Scenario: pickUpAxe
    Given create shelter "sh" with "Axe"
    Given create virologist "Virologist" on field "sh"
    When collect "Virologist"
    Then "Virologist" has equipment "Axe"

  Scenario: pickUpCloak
    Given create shelter "sh" with "Cloak"
    Given create virologist "Virologist" on field "sh"
    When collect "Virologist"
    Then "Virologist" has equipment "Cloak"
