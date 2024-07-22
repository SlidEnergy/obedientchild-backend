using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ObedientChild.WebApi.IntegrationTests
{
    /// <summary>
    /// Методы расширения для класса HttpResponseMessage проверяющие содержимое ответа.
    /// </summary>
    public static class HttpResponseMessageContentAssertExtensions
	{
		/// <summary>
		/// Проверяет, содержит ли ответ сущность с идентификатором.
		/// </summary>
		public static Task ContentIsEntity(this HttpResponseMessage response) => ContentIsEntity(response, "id");

		/// <summary>
		/// Проверяет, содержит ли ответ сущность с перечисленными свойствами.
		/// </summary>
		public static Task ContentIsEntity(this HttpResponseMessage response, params string[] expectedProperties) =>
			ContentIsEntity(response, null, expectedProperties);

		/// <summary>
		/// Проверяет, содержит ли ответ сущность с перечисленными свойствами и вызывает предикат для сущности.
		/// </summary>
		/// <param name="predicate">Функция, принимающая на вход сущность как словарь свойств, и возвращающая true или false.</param>
		public static async Task ContentIsEntity(this HttpResponseMessage response, Predicate<Dictionary<string, object>> predicate, params string[] expectedProperties)
		{
            Assert.That(response.Content, Is.Not.Null);
			var entity = await response.ToDictionary();
            Assert.That(entity, Is.Not.Null);

			AssertPropertiesExists(entity, expectedProperties);

			if (predicate != null)
                Assert.That(predicate(entity), Is.True);
		}

		/// <summary>
		/// Проверяет, содержит ли ответ сущность с перечисленными свойствами и вызывает предикат для сущности.
		/// </summary>
		/// <param name="predicate">Функция, принимающая на вход сущность как словарь свойств, и возвращающая true или false.</param>
		public static async Task ContentIsArray(this HttpResponseMessage response, int length)
		{
            Assert.That(response.Content, Is.Not.Null);
			var array = await response.ToArray();
            Assert.That(array, Is.Not.Null);
            Assert.That(array.Length, Is.EqualTo(length));
		}

		/// <summary>
		/// Проверяет, содержит ли ответ массив сущностей указанной длины.
		/// </summary>
		public static Task ContentIsArrayOfEntity(this HttpResponseMessage response, int minLength) => ContentIsArrayOfEntity(response, minLength, "id");

		/// <summary>
		/// Проверяет, содержит ли ответ массив сущностей указанной длины с перечисленными свойствами для каждой сущности.
		/// </summary>
		public static Task ContentIsArrayOfEntity(this HttpResponseMessage response, int minLength, params string[] expectedProperties) =>
			ContentIsArrayOfEntity(response, minLength, null, expectedProperties);

		/// <summary>
		/// Проверяет, содержит ли ответ массив сущностей с перечисленными свойствами для каждой сущности и вызывает предикат для каждой сущности.
		/// </summary>
		/// <param name="predicate">Функция, принимающая на вход сущность как словарь свойств, и возвращающая true или false.</param>
		public static async Task ContentIsArrayOfEntity(this HttpResponseMessage response, int minLength, Func<Dictionary<string, object>, bool> predicate, params string[] expectedProperties)
		{
            Assert.That(response.Content, Is.Not.Null);
			var array = await response.ToArrayOfDictionaries();
            Assert.That(array, Is.Not.Null);

			if (predicate != null) {
                Assert.That(array.Where(predicate).Count(), Is.GreaterThanOrEqualTo(minLength));
			} else {
                Assert.That(array.Length, Is.GreaterThanOrEqualTo(minLength));
			}

			foreach (var entity in array)
				AssertPropertiesExists(entity, expectedProperties);
		}

		/// <summary>
		/// Проверяет что переданная сущность содержит переданные свойства.
		/// </summary>
		/// <param name="entity">Сущность как словарь свойств</param>
		/// <param name="expectedProperties">Список свойств, которые должна содержать сущность</param>
		private static void AssertPropertiesExists(Dictionary<string, object> entity, string[] expectedProperties)
		{
			foreach (var prop in expectedProperties)
                Assert.That(entity.ContainsKey(prop), Is.True);
		}
	}
}
