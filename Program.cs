using Microsoft.AspNetCore.Http.HttpResults;
using NetMongoDb.Models;
using NetMongoDb.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<DatabaseSetting>(builder.Configuration.GetSection("MongoDatabase"));

builder.Services.AddSingleton<WorkoutService>();

var app = builder.Build();

app.MapGet("/", async (WorkoutService service) => await service.GetAsync());

app.MapGet("/{id}", async (string id, WorkoutService service) =>
{
    var workouts = await service.GetAsync(id);
    if (workouts is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(workouts);
});

app.MapPost("/create", async (Workouts workout, WorkoutService service) =>
{
    await service.CreateAsync(workout);
    return Results.Ok(workout);
}).Accepts<Workouts>("application/json", "application/xml");


app.MapPut("/update/{id}", async (string id, Workouts updatedWorkout, WorkoutService service) =>
{
    var workouts = await service.GetAsync(id);

    if (workouts is null)
    {
        return Results.NotFound();
    }

    updatedWorkout.Id = workouts.Id;

    await service.UpdateAsync(id, updatedWorkout);

    return Results.Ok("updated");
}).Accepts<Workouts>("application/json", "application/xml");
;

app.MapDelete("/delete/{id}", async (string id, WorkoutService service) =>
{
    var book = await service.GetAsync(id);

    if (book is null)
    {
        return Results.NotFound();
    }

    await service.RemoveAsync(id);

    return Results.Ok("deleted");
});


app.Run();
