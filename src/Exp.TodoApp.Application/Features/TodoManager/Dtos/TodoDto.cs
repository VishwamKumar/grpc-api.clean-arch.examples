﻿namespace Exp.TodoApp.Application.Features.TodoManager.Dtos;

public class TodoDto
{
    public int Id { get; set; }
    public string TodoName { get; private set; } = default!;   
}
