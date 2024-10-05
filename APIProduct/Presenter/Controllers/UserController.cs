using APIProduct.Domain.UseCases;
using APIProduct.Presenter.Entities;
using APIProduct.Presenter.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProduct.Presenter.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CreateUserUseCase _createUserUseCase;
        private readonly UserMapperPresenter _userMapperPresenter;
        private readonly GetAllUserUseCase _getAllUsersUseCase;
        private readonly GetUserByIdentityDocumentUseCase _getUserByIdentityDocumentUseCase;
        private readonly UpdateUserByIdentityDocumentUseCase _updateUserByIdentityDocumentUseCase;
        private readonly DeleteUserByIdentityDocumentUseCase _deleteUserByIdentityDocumentUseCase;

        public UserController(CreateUserUseCase createUserUseCase, 
            UserMapperPresenter userMapperPresenter, 
            GetAllUserUseCase getAllUsersUseCase, 
            GetUserByIdentityDocumentUseCase getUserByIdentityDocumentUseCase,
            UpdateUserByIdentityDocumentUseCase updateUserByIdentityDocumentUseCase,
            DeleteUserByIdentityDocumentUseCase deleteUserByIdentityDocumentUseCase
            )
        {
            _createUserUseCase = createUserUseCase;
            _userMapperPresenter = userMapperPresenter;
            _getAllUsersUseCase = getAllUsersUseCase;
            _getUserByIdentityDocumentUseCase = getUserByIdentityDocumentUseCase;
            _updateUserByIdentityDocumentUseCase = updateUserByIdentityDocumentUseCase;
            _deleteUserByIdentityDocumentUseCase = deleteUserByIdentityDocumentUseCase;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUser(UserPresenter userPresenter)
        {
            Domain.Entities.UserDomain userDomain = _userMapperPresenter.fromPresenterToDomain(userPresenter);
            Domain.Entities.UserDomain user = await _createUserUseCase.Execute(userDomain);
            return Ok(_userMapperPresenter.fromDomainToPresenter(user));

        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAllUsers()
        {
            List<Domain.Entities.UserDomain> users = await _getAllUsersUseCase.Execute();
            return Ok(users.Select(user => _userMapperPresenter.fromDomainToPresenter(user)));
        }

        [HttpGet]
        [Route("Get/{IdentityDocument}")]
        public async Task<IActionResult> GetUserByIdentityDocument(string IdentityDocument)
        {
            Domain.Entities.UserDomain user = await _getUserByIdentityDocumentUseCase.Execute(IdentityDocument);
            return Ok(_userMapperPresenter.fromDomainToPresenter(user));
        }

        [HttpPut]
        [Route("Update/{IdentityDocument}")]
        public async Task<IActionResult> UpdateUserByIdentityDocument(string IdentityDocument, UserPresenter userPresenter)
        {
            Domain.Entities.UserDomain userDomain = _userMapperPresenter.fromPresenterToDomain(userPresenter);
            Domain.Entities.UserDomain user = await _updateUserByIdentityDocumentUseCase.Execute(IdentityDocument, userDomain);
            return Ok(_userMapperPresenter.fromDomainToPresenter(user));
        }

        [HttpDelete]
        [Route("Delete/{IdentityDocument}")]
        public async Task<IActionResult> DeleteUserByIdentityDocument(string IdentityDocument)
        {
            Domain.Entities.UserDomain user = await _deleteUserByIdentityDocumentUseCase.Execute(IdentityDocument);
            return Ok(_userMapperPresenter.fromDomainToPresenter(user));
        }
    }
}

