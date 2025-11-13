namespace Exp.TodoApp.Application.Profiles;

public class TodoProfile : Profile
{
    public TodoProfile()
    {
        CreateMap<Todo, TodoDto>().ReverseMap();
    }
}
