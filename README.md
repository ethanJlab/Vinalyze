# Vinalyze
Vinalyze 

## App Description

Vinalyze is an open-source application designed to help you track and refine your wine preferences effortlessly.

How It Works
- Snap a picture of a wine you’re trying.
- Provide feedback: Like or dislike the wine, optionally rate it (1-5), and add notes on what you enjoy or dislike about it.
- Vinalyze builds a personalized wine profile based on your preferences.
- Get AI-driven recommendations for new wines tailored to your taste.

Whether you're a casual wine drinker or an aspiring sommelier, Vinalyze helps you discover wines you’ll love while keeping track of your journey. 

## Minimal Viable Product (MVP)

### Functionality
When using the application, the user should be able to:
- Create an account with a username, password, and email
- Take a picture of a wine and provide feedback
- See previous wines that they tried, along with the original image, and any provided feedback
- See previous "liked" wines
- See new wine recommendations  
- See an AI generated "Wine Profile Summary" with the total number of wines tried and a description of the users "taste".

## Nice to have
- A chat section to dicuss with an AI chatbot about wine with the context of the users profile

## TechStack
- Docker
  - This will be used to run any microservices or databases locally for development. It is assumed that these services will be moved to a cloud server for production with the necessary adjustments for scalability (i.e Kubernetes)
- Ms SQL Database
  - This will be used to store user data in a relational database.
~~- ASP .Net~~
-DJango
  -  This will be used to handel API calls to the database and external services
-  Liquor Control Board of Ontario (LCBO) Docker Image
   -  This is a public API from the Candian Gov't that can possibly be used to pull real data about wines that are being sold. The github repo is https://github.com/heycarsten/lcbo-api
