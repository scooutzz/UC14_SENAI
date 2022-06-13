﻿using ChapterWebApi.Controllers;
using ChapterWebApi.Interfaces;
using ChapterWebApi.Models;
using ChapterWebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestXunit.Controllers
{
    public class LoginControllerTest
    {

        [Fact]
        public void LoginController_Retornar_Usuario_Invalido()
        {
            // Arrange
            var fakeRepository = new Mock<IUsuarioRepository>();        // Mock
            fakeRepository.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns((Usuario)null);  // usuario invalido

            LoginViewModel dadosLogin = new LoginViewModel();
            dadosLogin.Email = "email@email.com";
            dadosLogin.Senha = "123";

            var controller = new LoginController(fakeRepository.Object);

            // Act
            var resultado = controller.Login(dadosLogin);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(resultado);
        }

        [Fact]
        public void LoginControler_Retornar_Token()
        {
            // Arrange
            Usuario usuarioRetorno = new Usuario();
            usuarioRetorno.Email = "email@email.com";
            usuarioRetorno.Senha = "1234";
            usuarioRetorno.Tipo = "0";

            var fakeRepository = new Mock<IUsuarioRepository>();
            fakeRepository.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(usuarioRetorno); // usuario valido

            string issuerValidacao = "chapter.webapi";

            LoginViewModel dadosLogin = new LoginViewModel();
            dadosLogin.Email = "email@email.com";
            dadosLogin.Senha = "123";

            var controller = new LoginController(fakeRepository.Object);

            // Act
            OkObjectResult resultado = (OkObjectResult)controller.Login(dadosLogin);

            string token = resultado.Value.ToString().Split(' ')[3];

            var jwtHandler = new JwtSecurityTokenHandler();
            var tokenJwt = jwtHandler.ReadJwtToken(token);

            // Assert
            Assert.Equal(issuerValidacao, tokenJwt.Issuer);
        }
    }
}
