namespace Domain.Shareds;

public class ApiResponse<T>
{
    public T Data { get; set; } = default!;

    public int HttpStatusCode { get; set; }

    public bool IsSuccess { get; set; }
}
