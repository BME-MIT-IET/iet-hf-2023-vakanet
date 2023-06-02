Feature: move
  Scenario: move
    Given create field "field"
    Given create virologist "v1" on field "field"
    Given create virologist "v2" on field "field"
    Given "v2" has "AminoAcid" 10

    When "v1" steals "AminoAcid" from "v2"

    Then "v1" must have "AminoAcid" 10
    Then "v2" must have "AminoAcid" 0