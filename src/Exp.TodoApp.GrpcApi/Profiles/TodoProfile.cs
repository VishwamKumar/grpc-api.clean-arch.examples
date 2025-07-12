namespace Exp.TodoApp.GrpcApi.Profiles;

public class TodoProfile : Profile
{
    public TodoProfile()
    {
        CreateMap<TodoData, TodoDto>().ReverseMap();
        CreateMap<CreateTodoRequest, CreateTodoDto>().ReverseMap();
        CreateMap<UpdateTodoRequest, UpdateTodoDto>().ReverseMap();
    }
}
