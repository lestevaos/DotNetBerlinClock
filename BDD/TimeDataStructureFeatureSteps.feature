Feature: The Time Data Structure
	As a programmer and Data Structure enthusiast
    I want to be able to store a Point in Time information in a Data Structure
    So that I can better represent Point in Time information inside the applications I create

# Parse

#   Expected successes

Scenario: Parse Midnight 00:00:00 (Beginning Of Day)
When the time is parsed as "00:00:00"
Then the programmer should get a parsed instance that looks like "00:00:00"

Scenario: Parse Morning 08:04:11
When the time is parsed as "08:04:11"
Then the programmer should get a parsed instance that looks like "08:04:11"

Scenario: Parse Noon 12:00:00
When the time is parsed as "12:00:00"
Then the programmer should get a parsed instance that looks like "12:00:00"

Scenario: Parse Afternoon 13:15:09
When the time is parsed as "13:15:09"
Then the programmer should get a parsed instance that looks like "13:15:09"

Scenario: Parse Late for Tea 17:00:01
When the time is parsed as "17:00:01"
Then the programmer should get a parsed instance that looks like "17:00:01"

Scenario: Parse Gym time 18:00:00
When the time is parsed as "18:00:00"
Then the programmer should get a parsed instance that looks like "18:00:00"

Scenario: Parse Midnight 24:00:00 (End Of Day)
When the time is parsed as "24:00:00"
Then the programmer should get a parsed instance that looks like "24:00:00"

#   /Expected successes

#   Expected errors

Scenario: Parse null
Then the programmer should get a parse error in the time portion

Scenario: Parse empty string
When the time is parsed as ""
Then the programmer should get a parse error in the time portion

Scenario: Parse whitespace string
When the time is parsed as " "
Then the programmer should get a parse error in the time portion

Scenario: Parse asdf
When the time is parsed as "asdf"
Then the programmer should get a parse error in the time portion

Scenario: Parse 11
When the time is parsed as "11"
Then the programmer should get a parse error in the time portion

Scenario: Parse 11:AA:00
When the time is parsed as "11:AA:00"
Then the programmer should get a parse error in the time portion

#   /Expected errors

# /Parse

# Ctor

#   Expected successes

Scenario: Ctor Midnight 00:00:00 (Beginning Of Day)
When the time is constructed using 0, 0, 0
Then the programmer should get a constructed instance that looks like "00:00:00"

Scenario: Ctor Morning 08:04:11
When the time is constructed using 8, 4, 11
Then the programmer should get a constructed instance that looks like "08:04:11"

Scenario: Ctor Noon 12:00:00
When the time is constructed using 12, 0, 0
Then the programmer should get a constructed instance that looks like "12:00:00"

Scenario: Ctor Afternoon 13:15:09
When the time is constructed using 13, 15, 9
Then the programmer should get a constructed instance that looks like "13:15:09"

Scenario: Ctor Late for Tea 17:00:01
When the time is constructed using 17, 0, 1
Then the programmer should get a constructed instance that looks like "17:00:01"

Scenario: Ctor Gym time 18:00:00
When the time is constructed using 18, 0, 0
Then the programmer should get a constructed instance that looks like "18:00:00"

Scenario: Ctor Midnight 24:00:00 (End Of Day)
When the time is constructed using 24, 0, 0
Then the programmer should get a constructed instance that looks like "24:00:00"

#   /Expected successes

#   Expected errors

#     Hours
Scenario: Ctor Hours < 0
When the time is constructed using -1, 0, 0
Then the programmer should get a constructor error in the hours portion

Scenario: Ctor Hours > 24
When the time is constructed using 25, 0, 0
Then the programmer should get a constructor error in the hours portion
#     /Hours

#     Minutes
Scenario: Ctor Minutes < 0
When the time is constructed using 0, -1, 0
Then the programmer should get a constructor error in the minutes portion

Scenario: Ctor Minutes > 59
When the time is constructed using 0, 60, 0
Then the programmer should get a constructor error in the minutes portion

Scenario: Ctor Hours = 24, minutes > 0
When the time is constructed using 24, 1, 0
Then the programmer should get a constructor error in the minutes portion
#     /Minutes

#     Seconds
Scenario: Ctor Seconds < 0
When the time is constructed using 0, 0, -1
Then the programmer should get a constructor error in the seconds portion

Scenario: Ctor Seconds > 59
When the time is constructed using 0, 0, 60
Then the programmer should get a constructor error in the seconds portion

Scenario: Ctor Hours = 24, seconds > 0
When the time is constructed using 24, 0, 1
Then the programmer should get a constructor error in the seconds portion
#     /Seconds

#   /Expected errors

# /Ctor
