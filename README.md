# VismaSolutions
A programming task given by the Sigma Solutions.

## Description (What I understood OR What this project does!)
- User will input starting date and ending date for applying for the holidays and system will return how much holidays a user can have and what are those days.
- User holidays doesn't includes National Holidays and Sundays.
- However, Saturdays are considered as user's holidays.
- Holidays can only be applied for the same period, i.e, 1 April - 31 March.
- After removing National holidays and Sundays, system check if remaining holidays are more than 50 days, it prompts that holidays can not be applied more than 50 days.

## Improvements that I would suggest
- National holidays must be handled dynamically instead of mentioning specifically, in upcoming versions.

## Unit Tests
There are also a test project in the solution which covers all the funcationality and methods to test all the corner test cases. For the purpose of testing I have used following libraries:

- xUnit
- NSubstitute
- FluentAssertion 
