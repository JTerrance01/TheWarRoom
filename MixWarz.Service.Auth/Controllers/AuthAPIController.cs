using MediatR;
using Microsoft.AspNetCore.Mvc;
using MixWarz.Service.Auth.Commands;
using MixWarz.Service.Auth.Models.Dto;
using MixWarz.Service.Auth.Querys;

namespace MixWarz.Service.Auth.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {        
        private readonly IMediator _mediator;
        private readonly ResponseDto _responseDto;
        public AuthAPIController(IMediator mediator)
        {           
            _mediator = mediator;
            _responseDto = new ResponseDto();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {            
            var response = await _mediator.Send(new RegisterCommand(model));
            
            if (!response.IsSuccess)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "User Creation Failed";
                return BadRequest(_responseDto);
            }            

            // Add logic to send Email Upon Registration via the MessageBus            

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _mediator.Send(new LoginQuery(model));
            
            if (loginResponse.User == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Username or password is incorrect";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = loginResponse;
            _responseDto.Message = $"{loginResponse.User.Email} Login Successful";
            return Ok(_responseDto);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var response = await _mediator.Send(new AssignRoleCommand(model));
            if (!response.IsSuccess)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Assigning Role Not Successful";
                return BadRequest(_responseDto);
            }
            _responseDto.IsSuccess = true;
            _responseDto.Message = "Role Selected Successfully";
            return Ok(_responseDto);
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            var forgotPasswordResponse = await _mediator.Send(new ForgotPasswordCommand(model));
            if (!forgotPasswordResponse.IsSuccess)
            {                
                return BadRequest(forgotPasswordResponse);
            }            
            return Ok(forgotPasswordResponse);
        }
    }
}
