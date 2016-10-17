USE [Restaurant]
GO
EXEC  sp_InsertCasherRecord  @id = 1,
						    @casher_name= 'Andrew',
							@casher_surname = 'Byron',
							@casher_login ='andrew.GoodFood',
							@casher_pass ='Andrew'

---------------------------------------------------
GO
EXEC sp_InsertGroupRecord @id=1,
						  @group_name='Snacks'

GO
EXEC sp_InsertGroupRecord @id=2,
						  @group_name='Salad'

GO
EXEC sp_InsertGroupRecord @id=3,
						  @group_name='Meat'

GO
EXEC sp_InsertGroupRecord @id=4,
						  @group_name='Soup'

GO
EXEC sp_InsertGroupRecord @id=5,
						  @group_name='Pizza'

GO
EXEC sp_InsertGroupRecord @id=6,
						  @group_name='Alcohol'

GO
EXEC sp_InsertGroupRecord @id=7,
						  @group_name='Drinks'

GO
EXEC sp_InsertGroupRecord @id=8,
						  @group_name='Fruits'

GO
EXEC sp_InsertGroupRecord @id=9,
						  @group_name='Desert'

GO 
-----------------------------------------------
GO
EXEC  sp_InsertMenuItem       @id=1,
							  @item_name='Hamburger',
							  @item_description='beef,onion,souse,tomato,salad leaves',
							  @item_price = 15,
							  @item_group = 1

GO
EXEC  sp_InsertMenuItem       @id=2,
							  @item_name='CheeseBurger',
							  @item_description='chesee,onion,souse,tomato,salad leaves',
							  @item_price = 17,
							  @item_group = 1
GO
EXEC  sp_InsertMenuItem       @id=3,
							  @item_name='Bread omele sandwitch',
							  @item_description='bread,onion,souse,eggs',
							  @item_price = 13,
							  @item_group = 1
GO
EXEC  sp_InsertMenuItem       @id=4,
							  @item_name='Mashroom sandwitch',
							  @item_description='beef,mashroom,tomato,salad leaves,souse',
							  @item_price = 20,
							  @item_group = 1
GO
EXEC  sp_InsertMenuItem       @id=5,
							  @item_name='Greek salad',
							  @item_description='tomatoes,cucambers,feta,olives',
							  @item_price = 22,
							  @item_group = 2
GO
EXEC  sp_InsertMenuItem       @id=6,
							  @item_name='Tomato-Peach Salad',
							  @item_description='Toss tomato and peach wedges with red onion slices.',
							  @item_price = 16,
							  @item_group = 2
GO
EXEC  sp_InsertMenuItem       @id=7,
							  @item_name='Caesar Salad',
							  @item_description='Purée minced garlic and anchovies, lemon juice, Worcestershire sauce, salt, pepper and 1 egg yolk; with machine running, slowly add 1/4 cup olive oil. ',
							  @item_price = 25,
							  @item_group = 2
GO
EXEC  sp_InsertMenuItem       @id=8,
							  @item_name='Hearty Tuna Salad',
							  @item_description='Mix cannellini beans, capers, pickled mushrooms, celery and olives; stir in mustard, lemon juice, salt and pepper.',
							  @item_price = 15,
							  @item_group = 2
GO
EXEC  sp_InsertMenuItem       @id=9,
							  @item_name='Chicken',
							  @item_description='4 onions, 2 unpeeled and thickly sliced, olive oil, plus extra for greasing, sweet smoked paprika, lemon thyme sprigs,1½ kg whole chicken',
							  @item_price = 55,
							  @item_group = 3
GO
EXEC  sp_InsertMenuItem       @id=10,
							  @item_name='Roast beef with red wine',
							  @item_description=' topside of beef, vegetable oil or sunflower oil,English mustard powder,Maldon sea salt, carrots, halved lengthways, wine, bay leaves',
							  @item_price = 95,
							  @item_group = 3
GO
EXEC  sp_InsertMenuItem       @id=11,
							  @item_name='Smoked duck & grilled peach salad',
							  @item_description=' extra virgin olive oil, sherry vinegar, peaches,watercress,smoked duck',
							  @item_price = 85,
							  @item_group = 3
GO
EXEC  sp_InsertMenuItem       @id=12,
							  @item_name='Bacon Bolognese',
							  @item_description='spaghetti, olive oil, carrot, celery sticks, smoked bacon tomato pesto',
							  @item_price = 42,
							  @item_group = 3
GO
EXEC  sp_InsertMenuItem       @id=13,
							  @item_name='Loaded Potato Soup',
							  @item_description='water,potato,onion,carrot, broccoli florets, diced fresh tomatoes',
							  @item_price = 10,
							  @item_group = 4
GO
EXEC  sp_InsertMenuItem       @id=14,
							  @item_name='New England Clam Chowder',
							  @item_description='potatoes, onion, and canned clams',
							  @item_price = 11,
							  @item_group = 4
GO
EXEC  sp_InsertMenuItem       @id=15,
							  @item_name='Tortilla Meatball Soup',
							  @item_description=' water,beans, rice, stews, canola mayonnaise.',
							  @item_price = 15,
							  @item_group = 4
GO
EXEC  sp_InsertMenuItem       @id=16,
							  @item_name='Broccoli and Cheese Soup',
							  @item_description='onion,cloves, cheese, milk,brocolli,chicken',
							  @item_price = 21,
							  @item_group = 4
GO
EXEC  sp_InsertMenuItem       @id=17,
							  @item_name='The Ultimate Pizza',
							  @item_description='onion,olive oil, rosamarine, tomatoes,cheese',
							  @item_price = 20,
							  @item_group = 5
GO
EXEC  sp_InsertMenuItem       @id=18,
							  @item_name='Four seasons',
							  @item_description='salami,becon,souse,mashrooms,tomatoes,cheese',
							  @item_price = 35,
							  @item_group = 5

GO
EXEC  sp_InsertMenuItem       @id=19,
							  @item_name='Four Cheeses',
							  @item_description='Souse,olive oil,4 different cheeses',
							  @item_price = 45,
							  @item_group = 5

GO
EXEC  sp_InsertMenuItem       @id=20,
							  @item_name='Havai',
							  @item_description='chicken,pineapple,souse,cheese',
							  @item_price = 30,
							  @item_group = 5
GO
EXEC  sp_InsertMenuItem       @id=21,
							  @item_name='Red wine',
							  @item_description='',
							  @item_price = 25,
							  @item_group = 6
GO
EXEC  sp_InsertMenuItem       @id=22,
							  @item_name='White wine',
							  @item_description='',
							  @item_price = 27,
							  @item_group = 6
GO
EXEC  sp_InsertMenuItem       @id=23,
							  @item_name='Beer',
							  @item_description='',
							  @item_price = 10,
							  @item_group = 6
GO
EXEC  sp_InsertMenuItem       @id=24,
							  @item_name='Wisky',
							  @item_description='',
							  @item_price = 40,
							  @item_group = 6
GO
EXEC  sp_InsertMenuItem       @id=25,
							  @item_name='Black tea',
							  @item_description='tea, lemon',
							  @item_price = 8,
							  @item_group = 7
GO
EXEC  sp_InsertMenuItem       @id=26,
							  @item_name='Green tea',
							  @item_description='tea, honey',
							  @item_price = 9,
							  @item_group = 7
GO
EXEC  sp_InsertMenuItem       @id=27,
							  @item_name='Fruit tea ',
							  @item_description='tea,orange,lemon',
							  @item_price = 13,
							  @item_group = 7
GO
EXEC  sp_InsertMenuItem       @id=28,
							  @item_name='Herbal tea',
							  @item_description='mint, chamomile,thyme',
							  @item_price = 15,
							  @item_group = 7
GO
EXEC  sp_InsertMenuItem       @id=29,
							  @item_name='Apple',
							  @item_description='',
							  @item_price = 10,
							  @item_group = 8
GO
EXEC  sp_InsertMenuItem       @id=30,
							  @item_name='Orange',
							  @item_description='',
							  @item_price = 14,
							  @item_group = 8
GO
EXEC  sp_InsertMenuItem       @id=31,
							  @item_name='Banana',
							  @item_description='',
							  @item_price = 12,
							  @item_group = 8
GO
EXEC  sp_InsertMenuItem       @id=32,
							  @item_name='Strawberry',
							  @item_description='',
							  @item_price = 16,
							  @item_group = 8
GO
EXEC  sp_InsertMenuItem       @id=33,
							  @item_name='Moist Chocolate Cake',
							  @item_description='chocolate cake with cream',
							  @item_price = 25,
							  @item_group = 9
GO
EXEC  sp_InsertMenuItem       @id=34,
							  @item_name='Banana cake',
							  @item_description='cake with banana cream',
							  @item_price = 15,
							  @item_group = 9
GO
EXEC  sp_InsertMenuItem       @id=35,
							  @item_name='Napoleon',
							  @item_description='Puff pastry cake with milk cream',
							  @item_price = 17,
							  @item_group = 9
GO
EXEC  sp_InsertMenuItem       @id=36,
							  @item_name='Drunk cherry',
							  @item_description='chocolate cake with cream and peaces of cherry',
							  @item_price = 20,
							  @item_group = 9



----------------------------------------------------------------
INSERT INTO [Table] VALUES (1,'Free');
INSERT INTO [Table] VALUES (2,'Free');
INSERT INTO [Table] VALUES (3,'Free');
INSERT INTO [Table] VALUES (4,'Free');
INSERT INTO [Table] VALUES (5,'Free');
INSERT INTO [Table] VALUES (6,'Free');
INSERT INTO [Table] VALUES (7,'Free');
INSERT INTO [Table] VALUES (8,'Free');
INSERT INTO [Table] VALUES (9,'Free');
INSERT INTO [Table] VALUES (10,'Free');
INSERT INTO [Table] VALUES (11,'Free');
INSERT INTO [Table] VALUES (12,'Free');
INSERT INTO [Table] VALUES (13,'Free');
INSERT INTO [Table] VALUES (14,'Free');
INSERT INTO [Table] VALUES (15,'Free');