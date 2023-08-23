using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ToDo;
using ToDo.DataBase;
using ToDo.Services.Classes;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Cors;
using System.Net.Http;


var builder = WebApplication.CreateBuilder();
builder.Services.AddCors();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAllOrigins",
//        builder => builder.WithOrigins("*")
//    );

//});

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ModelBase>(options => options.UseSqlServer(connection));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
}); 
var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());



app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors("AllowAll");
//app.UseCors("AllowAllOrigins");


app.MapGet("/api/tasks", async delegate (HttpContext context, ModelBase db) 
{
    QueryString queryString = context.Request.QueryString;

    return queryString.HasValue
        ? Helper.FilterTasks(db, queryString.Value == "?completed=true") 
        : db.Tasks.ToList();
} 
).RequireCors(options => options.AllowAnyOrigin());

app.MapPost("/api/tasks", async delegate (HttpContext context, ModelBase modelBase)
{
    Issue? taskToClass;
    try
    {
        taskToClass = await context.Request.ReadFromJsonAsync<Issue>();
        if (taskToClass != null)
        {
            await modelBase.AddAsync(taskToClass);
            await modelBase.SaveChangesAsync();
            context.Response.StatusCode = 201;
            return Results.Json(taskToClass);
        }
        context.Response.StatusCode = 400;
    }
    catch
    {
        context.Response.StatusCode = 400;
    }
    return null;
});


app.MapPatch("/api/tasks/{id}", async delegate (int id, HttpContext context, ModelBase modelBase)
{
    Issue? currentTask = modelBase.Tasks.FirstOrDefault(task => task.Id == id);
    Issue? request = await context.Request.ReadFromJsonAsync<Issue>();

    if (request != null && currentTask != null)
    {
        if (request.Description != null)
        {
            currentTask.Description = request.Description;
        }

        if (request.hasComplete != currentTask.hasComplete)
        {
            currentTask.hasComplete = request.hasComplete;
        }

        await modelBase.SaveChangesAsync();
        context.Response.StatusCode = 201;
    }
    else
        context.Response.StatusCode = 400;

    return currentTask;
});

app.MapDelete("/api/tasks/{id}", async delegate (int id, HttpContext context, ModelBase modelBase)
{
    Issue? currentTask = modelBase.Tasks.FirstOrDefault(u => u.Id == id);

    if(currentTask != null)
    {
        modelBase.Tasks.Remove(currentTask);
        await modelBase.SaveChangesAsync();
        return Results.StatusCode(201);
    }
    else
        return Results.StatusCode(204);
});

app.Run();



