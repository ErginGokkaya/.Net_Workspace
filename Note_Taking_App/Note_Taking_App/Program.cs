using Note_Taking_App.Models;
using Note_Taking_App.Datas;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<NoteDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Create a new note
app.MapPost("/notes", async (Note note, NoteDbContext context) =>
{
    note.CreatedAt = DateTime.UtcNow;
    note.UpdatedAt = DateTime.UtcNow;
    await context.Notes.AddAsync(note);
    await context.SaveChangesAsync();
    return Results.Created($"/notes/{note.Id}", note);
});

// Get all notes
app.MapGet("/notes", async (NoteDbContext context) => 
{
    var notes = await context.Notes.ToListAsync();
    return Results.Ok(notes);   
});

// Get a note by ID
app.MapGet("/notes/{id:int}", async (int id, NoteDbContext context) =>
{  
    var note = await context.Notes.FindAsync(id);
    return note is not null ? Results.Ok(note) : Results.NotFound();
});

// Update a note by ID
app.MapPut("/notes/{id:int}", async (int id, Note updatedNote, NoteDbContext context) =>
{
    var note = await context.Notes.FindAsync(id);
    if (note is null)
    {
        return Results.NotFound();
    }

    note.Title = updatedNote.Title;
    note.Content = updatedNote.Content;
    note.UpdatedAt = DateTime.UtcNow;

    await context.SaveChangesAsync();
    return Results.Ok(note);
});

// Delete a note by ID
app.MapDelete("/notes/{id:int}", async (int id, NoteDbContext context) =>
{
    var note = await context.Notes.FindAsync(id);
    if (note is null)
    {
        return Results.NotFound();
    }

    context.Notes.Remove(note);
    await context.SaveChangesAsync();
    return Results.NoContent();
});



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.Run();