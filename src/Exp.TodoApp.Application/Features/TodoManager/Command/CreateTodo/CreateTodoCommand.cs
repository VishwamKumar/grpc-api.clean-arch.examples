﻿namespace Exp.TodoApp.Application.Features.TodoManager.Command.CreateTodo;

public record CreateTodoCommand(CreateTodoDto CreateDto) : IRequest<int>;

