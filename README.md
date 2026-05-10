# Isio Developer Technical Test

This test revolves around a refactoring excercise called The Gilded Rose.

It can be completed in any of the following programming languages:
- C#
- JavaScript
- PHP
- Python
- TypeScript
- Visual Basic


## How to complete the test

Download the code in this repository and complete the test using your programming language of choice.
For each language, there is a unit test file and an example simulation program to help get you started.

When your solution is ready, send us the link to your submission (e.g. public Github repository or similar). 
Don't forget to include any additional instructions on how to build or run your solution, especially if you have introduced any new frameworks.

⚠️ Please do not submit PRs with test solutions directly against this source repository! They will be rejected.

As the task is refactoring based, a solution is widley open to developer interpretation and offers the opportunity for a candidate to showcase their broader skillset. 

There is no *correct* solution as such. Instead, you may be invited to justify your solution based on your approach and the technical design decisions you have made. You are welcome to include a new README file with your submission to assist with justification of your work.

Feel free to go above and beyond the initial requirements specification if desired. For example, you may think of an additional requirement and illustrate how your refactored code allows you to implement this efficiently.

You can spend as long as you like working on your submission, however a typical solution should take no more than 2-3 hours.

## Gilded Rose Requirements Specification

Hi and welcome to team Gilded Rose. As you may already know, we are a small inn with a prime location in a
prominent city ran by a friendly innkeeper. We also buy and sell only the finest goods.
Unfortunately, our goods are constantly degrading in `Quality` as they approach their sell by date.

We have a system in place that updates our inventory for us, however its codebase is very basic and is becoming increasingly difficult for us to maintain when we want to add or update functionality. 

Your task is to improve the quality of our codebase, and also add a new feature to our system so that we can begin selling a new category of items. First an introduction to our system:

- All `items` have a `SellIn` value which denotes the number of days we have to sell the `items`
- All `items` have a `Quality` value which denotes how valuable the item is
- At the end of each day our system lowers both values for every item

Pretty simple, right? Well this is where it gets interesting:

- Once the sell by date has passed, `Quality` degrades twice as fast
- The `Quality` of an item is never negative
- __"Aged Brie"__ actually increases in `Quality` the older it gets
- The `Quality` of an item is never more than `40`
- __"Sulfuras"__, being a legendary item, never has to be sold or decreases in `Quality`
- __"Backstage passes"__, like aged brie, increases in `Quality` as its `SellIn` value approaches;
	- `Quality` increases by `3` when there are `7` days or less and by `4` when there are `2` days or less but
	- `Quality` drops to `0` after the concert

We have recently signed a supplier of conjured items. This requires an update to our system:

- __"Conjured"__ items degrade in `Quality` twice as fast as normal items

Feel free to make any changes to the `UpdateQuality` method and add any new code as long as everything
still works correctly. However, you must __NOT__ alter the `Item` class or any of its properties.

## Submission Review
We are looking for candidates to successfully demonstrate the following criteria to be progressed to the stage in our application process:
1. Have shown an understanding and appreciation for the rules of the test
2. Solution compiles and/or runs on all assessor machines (using any additional and reasonable instructions provided) 
3. Have successfully implemented the new feature request
4. Demonstrated a clear attempt at code refactoring
5. Demonstrated a clear understanding of software testing
6. Solution contains few or zero bugs
7. Solution does NOT contain evidence of over-reliance on AI (please refrain from doing this, we are looking to assess your own skills and potential. Submissions that show over-reliance on AI will not be progressed) 
8. Solution does NOT contain signs of private, plagiarised, inappropriate or malicious material
9. Demonstrated technical capability in their submission, assessed through the following areas:
	1. Readability
	2. Maintainability
	3. Complexity
	4. Performance 
	5. Any documented design decision justifications


## Credits
The test source code is forked and adapted from the public original, which can be found here: https://github.com/emilybache/GildedRose-Refactoring-Kata 

## Candidates Notes 

### Assumptions

- Conjured is a modifier that can work on all items, if they decrease in quality.
- Backstage Passes work generically. Not only on passes for a specific concert

## Approach 

- First I added tests to confirm existing behavior.
- I noticed the readme specs did not match existing behavior doing this. 40 vs 50 quality cap, and the backstage thresholds were different.
- Once tests were in place, I then refactored the code to make the changes easy.
- Once the code was in a place where changes could be made easily, I then modified the code to 
match the specs, and add Conjured.
- I then added additional features like Ice Cream item to show how conjured works with all items 
automatically due to decorator + modifier system.
- I also added Backstage to vip area item, which forced me to define behavior when items names are similar

### Trade offs 

- Inside the ItemRegistry.FindUpdater method I've implemented some custom lookup logic and cache.
Originally this was just dictionary lookup(O(1)). However this could not support the
modifier(Conjured) system or handling backstage passes generically. This means the first lookup is
not as performant but the 2nd lookup will be O(1).

- I've implemented conjured as a decorator. This adds some complexity, but it does mean this
decorator can automatically work with all item updaters that decrease quality. For example 
I've added the ice cream item which degrades in a different way to standard items, and Conjured automatically works with that item without any changes.

- Using "Conjured" name by itself is just a standard item without the conjured modifier. For the modifier 
to work it needs to be combined with an existing item. This to avoid some additional code 
complexity. But it would not be hard to add.

- The way item matching works means that "Aged Brie Wheel of Cheddar" will match to the 
"Aged Brie" item updater. Not allowing this would break generic handling of backstage passes without additional code complexity and edge cases.

- Ice Cream + Backstage VIP area item updaters exist only to demonstrate how the
system works with new items. They could have implemented by parameterizing an existing 
ItemUpdater which have been simpler  


### Stuff I did not have time enough for

I was going to make the modifier system generic, so that it works with more than just conjured.
I.e You could register an additional modifier with a name, add it would automatically add the decorator to the item updater if the item name had a specified prefix.

