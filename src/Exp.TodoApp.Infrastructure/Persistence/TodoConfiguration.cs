namespace Exp.TodoApp.Infrastructure.Persistence;

public class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.ToTable("ToDos");
        builder.HasKey(e => e.Id);  // Set primary key

        builder.Property(e => e.Id)
       .HasColumnName("Id");

        builder.Property(e => e.TodoName)
              .HasColumnName("ToDoName")
              .HasMaxLength(100)
              .IsRequired();
    }
}
