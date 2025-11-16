global using System.Data;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.OpenApi.Models;
global using Google.Protobuf.WellKnownTypes;
global using AutoMapper;
global using Grpc.Core;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;

global using Exp.TodoApp.GrpcApi.Helpers;
global using Exp.TodoApp.GrpcApi.Services;
global using Exp.TodoApp.GrpcApi.Extensions;
global using Exp.TodoApp.GrpcApi.Middleware;
global using Exp.TodoApp.GrpcApi.Configuration;

global using Exp.TodoApp.Application.Extensions;
global using Exp.TodoApp.Application.Interfaces.CQRS;
global using Exp.TodoApp.Application.Common.Exceptions;
global using Exp.TodoApp.Application.Features.TodoManager.Dtos;
global using Exp.TodoApp.Application.Features.TodoManager.Queries.GetAll;
global using Exp.TodoApp.Application.Features.TodoManager.Queries.GetById;
global using Exp.TodoApp.Application.Features.TodoManager.Command.CreateTodo;
global using Exp.TodoApp.Application.Features.TodoManager.Command.UpdateTodo;
global using Exp.TodoApp.Application.Features.TodoManager.Command.DeleteTodo;

global using Exp.TodoApp.Domain.Common;
global using Exp.TodoApp.Infrastructure.Extensions;
global using Exp.TodoApp.Infrastructure.Persistence;