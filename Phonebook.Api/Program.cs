using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Phonebook.Application.Contacts;
using Phonebook.Application.DTOs;
using Phonebook.Infrastructure.Data;
using Phonebook.Infrastructure.Repositories;
using Phonebook.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// Mongo settings 
var mongoSettings = new MongoSettings();
builder.Configuration.GetSection("MongoSettings").Bind(mongoSettings);
builder.Services.AddSingleton(mongoSettings);
builder.Services.AddSingleton<MongoContext>();

// Repos
builder.Services.AddScoped<IContactRepository, ContactRepository>();

// MediatR 
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateContactCommand).Assembly));

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateContactRequest>();

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger em dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
