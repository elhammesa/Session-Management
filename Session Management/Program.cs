using Infrastructure.Context.command;
using Infrastructure.Context.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Session_Management.IOC;
using AutoMapper;
using MediatR;
using System.Reflection;
using Application.Base.Person.Commands;
using Dtos.Person;
using static Application.Base.Person.Commands.CancelSessionCommand;
using Application.Base.Session;
using Dtos.Session;
using static Application.Base.Session.SessionAddCommand;
using static Application.Base.Person.Commands.AddReportCommand;
using Services.Base;
using static Application.Base.Session.SessionEditCommand;
using static Application.Base.Person.Commands.ScheduleSessionCommand;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IRequestHandler<CancelSessionCommand, PersonCancelDto>, CancelSessionCommandHandler>();
builder.Services.AddScoped<IRequestHandler<SessionAddCommand, SessionAddDto>, SessionAddCommandHandler>();
builder.Services.AddScoped<IRequestHandler<AddReportCommand, PersonAddReportDto>, AddReportCommandCommandHandler>();
builder.Services.AddScoped<IRequestHandler<SessionEditCommand, SessionEditDto>, SessionEditCommandHandler>();
builder.Services.AddScoped<IRequestHandler<ScheduleSessionCommand, SessionscheduleDto>, ScheduleSessionCommandHandler>();

builder.Services.AddDbContext<CommandDataContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("CommandContionString"));

});
builder.Services.AddDbContext<QueryDataContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("QueryContionString"),
		options => options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));

});

builder.Services.Injector();
builder.Services.AddHostedService<ReminderService>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
