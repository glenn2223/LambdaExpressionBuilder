﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using LambdaExpressionBuilder.Interfaces;
using LambdaExpressionBuilder.Resources;
using ExpressionBuilder.Test.Models;
using NUnit.Framework;

namespace ExpressionBuilder.Test.Unit
{
    [TestFixture]
    public class PropertyLoaderTest
    {
        private readonly List<string> propertyIds = new List<string>
        {
            "Id", "Name", "Gender", "Salary", "Birth.Date", "Birth.DateOffset", "Birth.Age", "Birth.Country", "Contacts[Type]", "Contacts[Value]", "Contacts[Comments]", "Employer.Name", "Employer.Industry"
        };
        private readonly List<string> propertyNames = new List<string>
        {
            "Id", "Name", "Gender", "Salary", "Date of Birth", "DateOffset","Age", "Country of Birth", "Contact's Type", "Contact's Value", "Contact's Comments", "Employer's Name", "Employer's Industry"
        };
        private readonly List<string> propertyNamesptBr = new List<string>
        {
            "Id", "Nome", "Sexo", "Salário", "Data de nascimento", "DateOffset", "Idade", "País de origem", "Tipo de contato", "Valor do contato", "Comentários do contato", "Nome do empregador", "Indústria do empregador"
        };

        [TestCase("", TestName= "Loading properties' info")]
        [TestCase("pt-BR", TestName= "Loading properties' info")]
        public void PropertyLoaderLoadProperties(string cultureName)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            IPropertyCollection loader = new PropertyCollection(typeof(Person));
            var properties = loader.LoadProperties(Resources.Person.ResourceManager);
            var ids = properties.Select(p => p.Id);
            var names = properties.Select(p => p.Name);

            Assert.That(ids, Is.EquivalentTo(propertyIds));

            if (cultureName == "pt-BR")
            {
                Assert.That(names, Is.EquivalentTo(propertyNamesptBr));
            }
            else
            {
                Assert.That(names, Is.EquivalentTo(propertyNames));
            }
        }
    }
}