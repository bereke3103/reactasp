using aspnetserver.Controllers;
using aspnetserver.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDBContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/get-all-post", async () => await PostContoller.GetPostAsync()).
    WithTags("Post endpoints");

app.MapGet("/get-post-by-id/{postId}", async (int postId) =>
{
    Post postToReturn = await PostContoller.GetPostByIdAsync(postId);

    if (postToReturn != null)
    {
        return Results.Ok(postToReturn);
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Posts endpoints");


app.MapPost("/create-post", async (Post postToCreate) =>
{
    bool createSuccessfull = await PostContoller.CreatePostAsync(postToCreate);

    if (createSuccessfull)
    {
        return Results.Ok("Create succesfull");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Posts endpoints");

app.MapPut("/update-post", async (Post postToUpdate) =>
{
    bool updateSuccessfull = await PostContoller.UpdatePostAsync(postToUpdate);

    if (updateSuccessfull)
    {
        return Results.Ok("Update Successfull");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Posts endpoints");

app.MapDelete("/delete-post-id/{postId}", async (int postId) =>
{
    bool deletePostId = await PostContoller.DeletePostAsync(postId);

    if (deletePostId)
    {
        return Results.Ok("Delete successfull");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Posts endpoints");

app.Run();

