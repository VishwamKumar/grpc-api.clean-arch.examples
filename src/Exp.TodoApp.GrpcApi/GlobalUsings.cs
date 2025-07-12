global using Grpc.Core;
global using Grpc.Core.Interceptors;
global using Google.Protobuf;
global using Google.Protobuf.WellKnownTypes;
global using Google.Api;

global using System.Data;
global using System.Text;
global using System.Text.Json;
global using System.Diagnostics;
global using AutoMapper;
global using Microsoft.OpenApi.Models;
global using Microsoft.AspNetCore.Builder;
global using Exp.TodoApp.Application;
global using Exp.TodoApp.GrpcApi.Services;
global using Exp.TodoApp.GrpcApi.Extensions;
global using Exp.TodoApp.Application.Features.TodoManager.Dtos;

global using Exp.TodoApp.Application.Features.TodoManager.Queries.GetAll;
global using Exp.TodoApp.Application.Features.TodoManager.Queries.GetById;
global using Exp.TodoApp.Application.Features.TodoManager.Command.CreateTodo;
global using Exp.TodoApp.Application.Features.TodoManager.Command.UpdateTodo;
global using Exp.TodoApp.Application.Features.TodoManager.Command.DeleteTodo;
global using MediatR;