# GlideRush-Api

> Suggestions for backend api for leaderboards in Glide Rush
<hr>

A **leaderboard** contains a set of record atempts by various players.
Typically a leaderboard correlates with a single course and equivalent parameters so the various times can be compared.
A single course can have multiple leaderboards used for different occasions (eg an event).
Each **board entry** represents the best time on a specific board by a player.
The player is references by it's playfab id.

## See it in action

| Environment    | Swagger url  | CI/CD status |
| :------------- | :----------: | :---------------:  |
| Development    | https://[todo]/swagger/index.html | ![Build Status](https://dev.azure.com/[todo]?branchName=main) ![Deployment Status Dev]([todo]) |
| Demo / Staging | https://[todo]/swagger/index.html | ![Deployment Status demo]([todo]) |
| Production     | https://[todo]/swagger/index.html | ![Deployment Status prod][todo]) |

## How to contribute
### Environment

### Development config
Add overrides to the configuration in _appsettings.development.json_

### To _add a feature_ :hatching_chick:

1. add your feature to **GlideRush.Leaderboard.Service/Features** with
    * a _request_ (to get data) or a command (to create or alter data)
    This simply holds the data needed to start the request. Normally no methods are found on this class.
    * a _handler_ for the request that holds the logic to do the work
    This is where you would access the databases and services.
    The Handle(request) method is where the magic happens
    * a _result_ that holds the answer to the request or command.
    Here you can add the data in the most suitable format for this feature.
    For a request it is possibly the requested entities and maybe some precalculated values.
    For a command maybe the newly added entity, or the updated entity. You name it.
    Feel free to pull new forms of your data out of thin air best suiting your needs.
    * a _mapping_ class to map between the domain and the results.
    * a _validation_ class for validating your request class
    If there is nothing to validate you can leave the class empty.
    * an _authorization_ class for authorizing your request
    If there is nothing to authorize you can leave this empty.
    
1. add a controller endpoint in **GlideRush.Leaderboard.Api/Controllers**
Most endpoints only need to accept the request or command object.
If your endpoint only needs to accept a single basic value (eg an Id) you can create a request object here by adding the value to your request object. If you need to do more work to map the value to an object this would also be the place to do it.

1. add tests to GlideRush.Leaderboard.Service.Test/YourFeature/
  As most business related logic will be contained in your handler testing that one will probably be your primary test.
  Add a test class that implements ```IClassHandler<UnitTestFixture>```. Now you can use the fixture constructor argument to request the necessary services.
  The ```LeaderBoardContext``` context you receive here is an in-memory SQLLite context. It behaves mostly as the production database but is faster to construct and is re-created from scratch for every test.
  There are some slight implementation differences, is not meant for large data volumes and does not go through the migration steps so it is ill suited for integration or query performace tests.
  Use an environment as close to production as possible for those.

### To _extend the model_

1. add the right entities in **GlideRush.Leaderboard.Domain**
1. run `dotnet ef migrations add "yourMigrationNameHere" --startup-project="GlideRush.Leaderboard.Api" --project="GlideRush.Leaderboard.Persistence"`

## Talk plumbing to me :hot_pepper:

By using the onion architecture as a design guide where possible we are encouraged to keep library responsabilities focused
1. GlideRush.Leaderboard.Domain
  Contains only our domain classes used inside our libraries. These are dumb property bags and don't contain operations so don't need to be tested

1. GlideRush.Leaderboard.Persistence
  Contains everything related to persist and fetch our data. In practice this means efcore. This libarry references the domain libary.  As this is also mostly plumbing it also needs little testing.

2. GlideRush.Leaderboard.Service
  This is the interesting bit. This one contains the business logic. It references persistance to get/save the data it needs to work.  This is the one you should want to test thoroughly.

3. GlideRush.Leaderboard.Api
  This is one of the user facing layers. Here we do the work to handle REST and REST-related work and transform it into things our business can work with and responses our users can work with too. This should need little testing beyond plumbing testing.

Leveraging mediatr to pull our features into pipelines we encourage the use of small, focused classes.
A normal flow goes like this request => authorize => validate => handle => response

Although mediatr is not strictly necessary to make this possible it does guide contributors through the request process
   * It keeps controller responsibilities limited to REST and REST adjacent things
   * It keeps service dependencies closer to the actual logic in our handlers
   * It nudges to think about validation, authorization and response mapping

By isolating our request parameters in a request or command and combining that with Fluent validator we separate validation from the rest and allow for more complex validation logic
By defining automapper with the right conventions we limit boilerplate mapping to a minimum
By making authorization and authentication part of our request pipeline we can never forget and can write a single test on the pipelinebehaviour instead of rewriting every authorization line again in a unit test.