global using System.Data;
global using Microsoft.OpenApi.Models;

global using Exp.TodoApp.GrpcApi.Helpers;
global using Exp.TodoApp.GrpcApi.Services;
global using Exp.TodoApp.GrpcApi.Extensions;

global using Exp.TodoApp.Application.Extensions;
global using Exp.TodoApp.Application.Common.Exceptions;
global using Exp.TodoApp.Application.Features.TodoManager.Dtos;
global using Exp.TodoApp.Application.Features.TodoManager.Queries.GetAll;
global using Exp.TodoApp.Application.Features.TodoManager.Queries.GetById;
global using Exp.TodoApp.Application.Features.TodoManager.Command.CreateTodo;
global using Exp.TodoApp.Application.Features.TodoManager.Command.UpdateTodo;
global using Exp.TodoApp.Application.Features.TodoManager.Command.DeleteTodo;

global using Exp.TodoApp.Domain.Common;
global using Exp.TodoApp.Infrastructure.Extensions;

global using Google.Protobuf.WellKnownTypes;
global using AutoMapper;
global using MediatR;
global using Grpc.Core;