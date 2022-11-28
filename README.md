# Lara's Comments

API: 
  Architecture: Quickly done DDD. When entities for an application have such clear business logic, I perfer to use a variation of this architecture, keeping busines logic on the enties themselves, though this can sometimes cause some bloat.
  Validation: I had no time to implement any validation, though I would have used Fluent Validation if I had.
  Various things of note: I decided to keep the state management at the repo level for this project, thus making the return objects, and  building the, a bit awkard and redundant. I have little, and bad, error handling in the project, and I did not have time to implement several things that would have reduced repeated code - an interface for the crud operations, a base controller. I have noted some thoroughout the code. Also, the paylocity nuget packages were unavailable, so I replaced them all with the npm registry in the package.lock.json.

React app: 
  Architecture: I created a structure with components, models, services, and utils. With more time I would have added a store to handle state management.
  I would have liked to have converted this project to use TS, especially to allow for it to be strongly typed.
  
## To Run
  To run the API from Visual Studio, set the startup project to API and run.
  
  To run the React app, from the app folder, run <code>npm start</code>.

# What is this?

A project seed containing a React app ("app") with a C# dotnet API ("PaylocityBenefitsCalculator").  It is meant to get you started on the Paylocity Coding Challenge by taking some initial setup decisions away.

The goal is to respect your time, avoid live coding, and get a sense for how you work.

# Coding Challenge

**Show us how you work.**

Each of our Paylocity product teams operates like a small startup, empowered to deliver business value in
whatever way they see fit. Because our teams are close knit and fast moving it is imperative that you are able
to work collaboratively with your fellow developers. 

This coding challenge is designed to allow you to demonstrate your abilities and discuss your approach to
design and implementation with your potential colleagues. You are free to use whatever technologies you
prefer but please be prepared to discuss the choices you’ve made. We encourage you to focus on creating a
logical and functional solution rather than one that is completely polished and ready for production.

The challenge can be used as a canvas to capture your strengths in addition to reflecting your overall coding
standards and approach. There’s no right or wrong answer.  It’s more about how you think through the
problem. We’re looking to see your skills in all three tiers so the solution can be used as a conversation piece
to show our teams your abilities across the board.

Requirements will be given separately.
