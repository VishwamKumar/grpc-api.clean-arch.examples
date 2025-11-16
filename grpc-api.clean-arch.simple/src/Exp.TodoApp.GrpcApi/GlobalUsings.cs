global using System.Data;
global using Microsoft.Extensions.Logging;
global using Microsoft.OpenApi.Models;
global using Serilog;
global using Grpc.Core.Interceptors;

global using Exp.TodoApp.GrpcApi.Helpers;
global using Exp.TodoApp.GrpcApi.Services;
global using Exp.TodoApp.GrpcApi.Extensions;
global using Exp.TodoApp.GrpcApi.Middleware;
global using Exp.TodoApp.GrpcApi.Interceptors;

global using Exp.TodoApp.Application.Extensions;
global using Exp.TodoApp.Application.Common.Exceptions;
global using Exp.TodoApp.Application.Dtos;
global using Exp.TodoApp.Application.Services;

global using Exp.TodoApp.Infrastructure.Extensions;
global using Exp.TodoApp.Domain.Common;

global using Google.Protobuf.WellKnownTypes;
global using AutoMapper;
global using Grpc.Core;

