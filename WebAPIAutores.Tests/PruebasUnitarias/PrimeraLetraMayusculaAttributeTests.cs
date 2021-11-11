using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using WebAPIAutores.Validaciones;

namespace WebAPIAutores.Tests.PruebasUnitarias
{
    [TestClass]
    public class PrimeraLetraMayusculaAttributeTests
    {
        [TestMethod]
        public void PrimeraLetraMinuscula_DevuelveError()
        {
            //given
            var PrimeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();
            var valor = "santiago";
            var valContext = new ValidationContext(new { nombre = valor});
            //when
            var resultado = PrimeraLetraMayuscula.GetValidationResult(valor, valContext);

            //then
            Assert.AreEqual("La primera letra debe ser mayúscula", resultado.ErrorMessage);
        }
        [TestMethod]
        public void ValorNulo_NoDevuelveError()
        {
            //given
            var PrimeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();
            string valor = null;
            var valContext = new ValidationContext(new { nombre = valor });
            //when
            var resultado = PrimeraLetraMayuscula.GetValidationResult(valor, valContext);

            //then
            Assert.IsNull(resultado);
        }
        [TestMethod]
        public void ValorConPrimeraLetraMayuscula_NoDevuelveError()
        {
            //given
            var PrimeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();
            string valor = "Texto sin error";
            var valContext = new ValidationContext(new { nombre = valor });
            //when
            var resultado = PrimeraLetraMayuscula.GetValidationResult(valor, valContext);

            //then
            Assert.IsNull(resultado);
        }

    }
}