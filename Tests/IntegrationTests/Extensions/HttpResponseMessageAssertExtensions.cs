using NUnit.Framework;
using System.Net;
using System.Net.Http;

namespace ObedientChild.WebApi.IntegrationTests
{
    /// <summary>
    /// Методы расширения для класса HttpResponseMessage проверяющие статус ответа.
    /// </summary>
    public static class HttpResponseMessageAssertExtensions
	{
		/// <summary>
		/// Проверяет содержит ли ответ статус BadRequest.
		/// </summary>
		public static void IsBadRequest(this HttpResponseMessage response)
		{
			Assert.That(response, Is.Not.Null);
			Assert.That(HttpStatusCode.BadRequest, Is.EqualTo(response.StatusCode));
		}

		/// <summary>
		/// Проверяет содержит ли ответ статус NotFound.
		/// </summary>
		public static void IsNotFound(this HttpResponseMessage response)
		{
			Assert.That(response, Is.Not.Null);
            Assert.That(HttpStatusCode.NotFound, Is.EqualTo(response.StatusCode));
		}

		/// <summary>
		/// Проверяет содержит ли ответ статус Unauthorized.
		/// </summary>
		public static void IsUnauthorized(this HttpResponseMessage response)
		{
            Assert.That(response, Is.Not.Null);
            Assert.That(HttpStatusCode.Unauthorized, Is.EqualTo(response.StatusCode));
		}

		/// <summary>
		/// Проверяет содержит ли ответ успешный статус.
		/// </summary>
		public static void IsSuccess(this HttpResponseMessage response)
		{
            Assert.That(response, Is.Not.Null);
            Assert.That(response.IsSuccessStatusCode, Is.True);
		}
	}
}
