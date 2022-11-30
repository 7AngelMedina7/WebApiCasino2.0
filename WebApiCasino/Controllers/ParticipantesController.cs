using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiCasino.DTOs;
using WebApiCasino.DTOs.Autenticacion;
using WebApiCasino.Entidades;
//using WebApiCasino.Filtros;

//namespace WebApiCasino.Controllers
//{
//    [ApiController]
//    [Route("/Participantes")]
//    [TypeFilter(typeof(ExceptionManagerFiltro))]
//    public class ParticipantesController : ControllerBase
//    {
//        public readonly ApplicationDbContext dbContext;
//        public readonly IMapper mapper;
//        private readonly UserManager<IdentityUser> userManager;
//        private readonly IConfiguration configuration;
//        private readonly SignInManager<IdentityUser> signInManager;

//        public ParticipantesController(UserManager<IdentityUser> userManager, IConfiguration configuration,
//            SignInManager<IdentityUser> signInManager, ApplicationDbContext dbContext, IMapper mapper)
//        {
//            this.mapper = mapper;
//            this.dbContext = dbContext;
//            this.userManager = userManager;
//            this.configuration = configuration;
//            this.signInManager = signInManager;
//        }

//    }
//}
