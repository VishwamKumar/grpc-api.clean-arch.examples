﻿namespace Exp.TodoApp.Application.Features.TodoManager.Dtos;

public class UpdateTodoDto
{
    public int Id { get; set; }
    public string TodoName { get; set; } = default!;   
}
