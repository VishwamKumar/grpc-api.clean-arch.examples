global using AutoMapper;
global using FluentValidation;
global using FluentValidation.Results;
global using System.Reflection;
global using Microsoft.Extensions.DependencyInjection;

global using Exp.TodoApp.Domain.Entities;
global using Exp.TodoApp.Domain.Common;
global using Exp.TodoApp.Application.Features.TodoManager.Dtos;
global using Exp.TodoApp.Application.Features.TodoManager.Command.CreateTodo;
global using Exp.TodoApp.Application.Features.TodoManager.Command.UpdateTodo;
global using Exp.TodoApp.Application.Features.TodoManager.Command.DeleteTodo;
global using Exp.TodoApp.Application.Interfaces.Persistence;
global using Exp.TodoApp.Application.Interfaces.CQRS;
global using Exp.TodoApp.Application.Common;
global using Exp.TodoApp.Application.Common.Exceptions;
