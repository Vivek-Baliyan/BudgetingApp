using API.Repository.Interfaces;
using API.Services.Interfaces;

namespace API.Controllers
{
    public class UsersController(IUnitOfWork unitOfWork, ITokenService tokenService) : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ITokenService _tokenService = tokenService;
    }
}