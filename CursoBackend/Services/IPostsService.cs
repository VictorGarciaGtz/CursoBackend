using CursoBackend.DTOs;

namespace CursoBackend.Services
{
    public interface IPostsService
    {
        public Task<IEnumerable<PostDto>> Get();
    }
}
