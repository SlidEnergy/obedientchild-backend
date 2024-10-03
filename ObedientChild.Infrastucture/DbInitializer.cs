using ObedientChild.Domain;
using ObedientChild.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using ObedientChild.App;
using ObedientChild.App.Personalities;
using ObedientChild.Domain.Personalities;

namespace ObedientChild.Infrastucture
{
    public class DbInitializer
	{
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _services;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<DbInitializer> _logger;
		private readonly ILifeEnergyService _lifeEnergyService;
		private readonly IPersonalitiesService _personalitiesService;
		private readonly ICharacterTraitsService _characterTraitsService;

        public DbInitializer(IServiceProvider services)
        {  
			_services = services;

            _context = _services.GetRequiredService<ApplicationDbContext>();
			_userManager = _services.GetRequiredService<UserManager<ApplicationUser>>();
			_roleManager = _services.GetRequiredService<RoleManager<IdentityRole>>();
			_logger = _services.GetRequiredService<ILogger<DbInitializer>>();
			_lifeEnergyService = _services.GetRequiredService<ILifeEnergyService>();
			_personalitiesService = _services.GetRequiredService<IPersonalitiesService>();
			_characterTraitsService = _services.GetRequiredService<ICharacterTraitsService>();
		}

		public async Task Initialize()
		{
			_context.Database.EnsureCreated();

			var user = await CreateDefaultUserAndRoleAsync();

            await CreateLifeEnergyAccountAsync(user);

			await CreatePersonalitiesAsync();
        }

		private async Task<ApplicationUser> CreateDefaultUserAndRoleAsync()
		{
			const string email = "admin@mail.ru";
			const string password = "admin";

			// Берем первого созданного пользователя
			var user = await _context.Users.FirstOrDefaultAsync();

			// Для пустой базы создаем нового пользователя
			if (user == null)
			{
				user = await CreateDefaultUser(email);

				// Временно отключаем проверку пароля для администратора
				var passwordValidators = _userManager.PasswordValidators;
				_userManager.PasswordValidators.Clear();

				await SetPasswordForUser(email, user, password);

				// Возвращаем проверку пароля обратно
				foreach (var validator in passwordValidators)
					_userManager.PasswordValidators.Add(validator);
			}

			var role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == Role.Admin);

			// Создаем группу администратора, если в системе еще не было групп и устанавливаем её для пользователя
			// Если ранее был пользователь но не было группы, то для первого созданного пользователя будет установалена группа администратора
			if (role == null)
			{
				await CreateDefaultAdministratorRole(Role.Admin);

				await AddRoleToUser(email, Role.Admin, user);
			}

			return user;
		}

		private async Task CreateDefaultAdministratorRole(string administratorRole)
		{
			_logger.LogInformation($"Create the role `{administratorRole}` for application");
			var result = await _roleManager.CreateAsync(new IdentityRole(administratorRole));
			if (result.Succeeded)
			{
				_logger.LogDebug($"Created the role `{administratorRole}` successfully");
			}
			else
			{
				var exception = new ApplicationException($"Default role `{administratorRole}` cannot be created");
				_logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(result));
				throw exception;
			}
		}

		private async Task<ApplicationUser> CreateDefaultUser(string email)
		{
			_logger.LogInformation($"Create default user with email `{email}` for application");
			var user = new ApplicationUser(email);

			var result = await _userManager.CreateAsync(user);
			if (result.Succeeded)
			{
				_logger.LogDebug($"Created default user `{email}` successfully");
			}
			else
			{
				var exception = new ApplicationException($"Default user `{email}` cannot be created");
				_logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(result));
				throw exception;
			}

			var createdUser = await _userManager.FindByEmailAsync(email);
			return createdUser;
		}

		private async Task SetPasswordForUser(string email, ApplicationUser user, string password)
		{
			_logger.LogInformation($"Set password for default user `{email}`");
			var result = await _userManager.AddPasswordAsync(user, password);
			if (result.Succeeded)
			{
				_logger.LogTrace($"Set password `{password}` for default user `{email}` successfully");
			}
			else
			{
				var exception = new ApplicationException($"Password for the user `{email}` cannot be set");
				_logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(result));
				throw exception;
			}
		}

		private async Task AddRoleToUser(string email, string administratorRole, ApplicationUser user)
		{
			_logger.LogInformation($"Add default user `{email}` to role '{administratorRole}'");
			var result = await _userManager.AddToRoleAsync(user, administratorRole);
			if (result.Succeeded)
			{
				_logger.LogDebug($"Added the role '{administratorRole}' to default user `{email}` successfully");
			}
			else
			{
				var exception = new ApplicationException($"The role `{administratorRole}` cannot be set for the user `{email}`");
				_logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(result));
				throw exception;
			}
		}

		private static string GetIdentiryErrorsInCommaSeperatedList(IdentityResult result) =>
			Lers.Utils.ArrayUtils.JoinToString(result.Errors.Select(x => x.Description), ", ");

        private async Task CreateLifeEnergyAccountAsync(ApplicationUser user)
        {
			var account = await _lifeEnergyService.GetAccountWithAccessCheckAsync(user.Id);

			if (account == null)
			{
				await _lifeEnergyService.CreateAccountAsync(user.Id);
			}
        }

        private async Task CreatePersonalitiesAsync()
        {
            var destinies = await _personalitiesService.GetDestinyListAsync();

            if (destinies == null || destinies.Count > 0)
                return;                

            // add all destinies

			// лидер
            var destiny1 = await _personalitiesService.AddDestinyAsync(new Destiny("Руководитель компании"));
            var destiny2 = await _personalitiesService.AddDestinyAsync(new Destiny("Управляющий проектами"));
            var destiny3 = await _personalitiesService.AddDestinyAsync(new Destiny("Лидер команды"));
            var destiny4 = await _personalitiesService.AddDestinyAsync(new Destiny("Политик"));

			// творческая личность
            var destiny5 = await _personalitiesService.AddDestinyAsync(new Destiny("Художник"));
            var destiny6 = await _personalitiesService.AddDestinyAsync(new Destiny("Писатель"));
            var destiny7 = await _personalitiesService.AddDestinyAsync(new Destiny("Графический дизайнер"));
            var destiny8 = await _personalitiesService.AddDestinyAsync(new Destiny("Изобретатель"));
            var destiny9 = await _personalitiesService.AddDestinyAsync(new Destiny("Режиссер"));

			// гуманист
            var destiny10 = await _personalitiesService.AddDestinyAsync(new Destiny("Социальный работник"));
            var destiny11 = await _personalitiesService.AddDestinyAsync(new Destiny("Врач"));
            var destiny12 = await _personalitiesService.AddDestinyAsync(new Destiny("Учитель"));
            var destiny13 = await _personalitiesService.AddDestinyAsync(new Destiny("Волонтер"));
            var destiny14 = await _personalitiesService.AddDestinyAsync(new Destiny("Терапевт"));

			// исследователь
            var destiny15 = await _personalitiesService.AddDestinyAsync(new Destiny("Научный сотрудник"));
            var destiny16 = await _personalitiesService.AddDestinyAsync(new Destiny("Профессор"));
            var destiny17 = await _personalitiesService.AddDestinyAsync(new Destiny("Лаборант"));
            var destiny18 = await _personalitiesService.AddDestinyAsync(new Destiny("Эксперт по данным"));
            var destiny19 = await _personalitiesService.AddDestinyAsync(new Destiny("Эксперт в определенной области"));

			// предприниматель
            var destiny20 = await _personalitiesService.AddDestinyAsync(new Destiny("Основатель стартапа"));
            var destiny21 = await _personalitiesService.AddDestinyAsync(new Destiny("Бизнесмен"));
            var destiny22 = await _personalitiesService.AddDestinyAsync(new Destiny("Владельц малого бизнеса"));
            var destiny23 = await _personalitiesService.AddDestinyAsync(new Destiny("Инвестор"));

			// мотиватор
            var destiny24 = await _personalitiesService.AddDestinyAsync(new Destiny("Мотивационный спикер"));
            var destiny25 = await _personalitiesService.AddDestinyAsync(new Destiny("Наставник"));
            var destiny26 = await _personalitiesService.AddDestinyAsync(new Destiny("Коуч"));
            var destiny27 = await _personalitiesService.AddDestinyAsync(new Destiny("Тренер по личностному развитию"));

			// аналитик
            var destiny28 = await _personalitiesService.AddDestinyAsync(new Destiny("Бизнес-аналитик"));
            var destiny29 = await _personalitiesService.AddDestinyAsync(new Destiny("Стратегический консультант"));
            var destiny30 = await _personalitiesService.AddDestinyAsync(new Destiny("Специалист по данным"));
            var destiny31 = await _personalitiesService.AddDestinyAsync(new Destiny("Финансовый аналитик"));


            // add all personalities
            var personality1 = await _personalitiesService.AddAsync(
                new[] { destiny1.Id, destiny2.Id, destiny3.Id, destiny4.Id },
                new Personality("Лидер")
                {
                    Description = "Личность, способная вдохновлять и направлять других, брать на себя ответственность и принимать сложные решения."
                });

            var personality2 = await _personalitiesService.AddAsync(
                new[] { destiny5.Id, destiny6.Id, destiny7.Id, destiny8.Id, destiny9.Id },
                new Personality("Творческая Личность")
                {
                    Description = "Человек, который генерирует новые идеи и подходит к жизни с нестандартной перспективой."
                });

            var personality3 = await _personalitiesService.AddAsync(
                new[] { destiny10.Id, destiny11.Id, destiny12.Id, destiny13.Id, destiny14.Id },
                new Personality("Гуманист")
                {
                    Description = "Личность, ориентированная на помощь другим, проявляющая эмпатию и заботу."
                });

            var personality4 = await _personalitiesService.AddAsync(
                new[] { destiny15.Id, destiny16.Id, destiny17.Id, destiny18.Id, destiny19.Id },
                new Personality("Исследователь")
                {
                    Description = "Личность, обладающая глубоким стремлением к изучению мира, науке и пониманию новых знаний."
                });

            var personality5 = await _personalitiesService.AddAsync(
                new[] { destiny20.Id, destiny21.Id, destiny22.Id, destiny23.Id },
                new Personality("Предприниматель")
                {
                    Description = "Человек, готовый к риску, активно ищущий возможности и обладающий способностью к созданию чего-то нового."
                });

            var personality6 = await _personalitiesService.AddAsync(
                new[] { destiny24.Id, destiny25.Id, destiny26.Id, destiny27.Id },
                new Personality("Мотиватор")
                {
                    Description = "Личность, способная вдохновлять и поддерживать других, мотивировать на действие."
                });

            var personality7 = await _personalitiesService.AddAsync(
                new[] { destiny28.Id, destiny29.Id, destiny30.Id, destiny31.Id },
                new Personality("Аналитик")
                {
                    Description = "Человек, который любит анализировать ситуации и решать сложные задачи."
                });

            // add all character traits and their levels
            // Лидер
            await AddCharacterTraitWithLevelsAsync(new[] { personality1.Id }, "Уверенность", "Новичок, Уверенный, Решительный, Лидер, Харизматичный Лидер", "Способность верить в себя и свои силы, принимать важные решения.");
            await AddCharacterTraitWithLevelsAsync(new[] { personality1.Id }, "Ответственность", "Новичок, Студент, Специалист, Лидер, Объединитель", "Готовность брать на себя ответственность за свои действия и решения.");

            // Творческая Личность
            await AddCharacterTraitWithLevelsAsync(new[] { personality2.Id }, "Креативность", "Новичок, Искатель, Воображение, Художник, Вдохновляющий Творец", "Способность генерировать оригинальные идеи и находить нестандартные решения.");// await AddCharacterTraitWithLevelsAsync(new[] { personality2.Id }, "Открытость", "Начинающий, Открытый Ум, Исследователь, Творец, Эстет"); // -

            // Гуманист
            await AddCharacterTraitWithLevelsAsync(new[] { personality3.Id }, "Доброта", "Новичок, Заботливый, Помощник, Эмпат, Соратник", "Проявление заботы и понимания к другим, готовность помогать.");
            await AddCharacterTraitWithLevelsAsync(new[] { personality3.Id }, "Отзывчивость", "Начинающий, Заботящийся, Ободряющий, Поддерживающий, Вдохновляющий", "Готовность откликаться на нужды и просьбы других.");
            await AddCharacterTraitWithLevelsAsync(new[] { personality3.Id }, "Бескорыстность", "Новичок, Доброжелательный, Ведущий, Наставник, Гуру Доброты", "Способность помогать другим без ожидания чего-либо взамен.");

            // Исследователь
            await AddCharacterTraitWithLevelsAsync(new[] { personality4.Id }, "Наблюдательность", "Новичок, Внимательный, Наблюдающий, Аналитик, Гуру Наблюдения", "Умение замечать детали и анализировать ситуацию.");
            
            // Предприниматель
            //await AddCharacterTraitWithLevelsAsync(new[] { personality5.Id }, "Рисковость", "Новичок, Уверенный, Рисковый, Предприниматель, Инноватор"); // +решительность
            await AddCharacterTraitWithLevelsAsync(new[] { personality5.Id }, "Инициативность", "Новичок, Студент, Активист, Лидер, Пионер", "Готовность брать инициативу на себя, находить новые возможности.");
            await AddCharacterTraitWithLevelsAsync(new[] { personality5.Id }, "Коммуникабельность", "Начинающий, Общительный, Оратор, Вдохновляющий Лидер, Наставник", "Способность эффективно общаться и строить отношения с другими.");
            await AddCharacterTraitWithLevelsAsync(new[] { personality5.Id }, "Эффективность", "Новичок, Рациональный, Продуктивный, Управляющий Временем, Мастер Тайм-менеджмента", "Умение рационально использовать время и ресурсы.");

            // Мотиватор
            //await AddCharacterTraitWithLevelsAsync(new[] { personality6.Id }, "Мотивация", "Новичок, Воодушевляющий, Мотиватор, Вдохновляющий, Лидер"); // харизматичность
            await AddCharacterTraitWithLevelsAsync(new[] { personality6.Id }, "Оптимизм", "Новичок, Позитивный, Ободряющий, Вдохновляющий, Гуру Оптимизма", "Способность сохранять позитивный настрой и вдохновлять окружающих."); // может что-то другое?
            //await AddCharacterTraitWithLevelsAsync(new[] { personality6.Id }, "Энергия", "Начинающий, Энергичный, Вдохновляющий, Лидер, Мотиватор"); // активность

            // Аналитик
            await AddCharacterTraitWithLevelsAsync(new[] { personality7.Id }, "Аналитическое мышление", "Новичок, Аналитик, Исследователь, Эксперт, Гуру Анализа", "Умение разбирать информацию на составляющие, делать выводы и находить закономерности.");// прагматичность
            await AddCharacterTraitWithLevelsAsync(new[] { personality7.Id }, "Внимание к деталям", "Новичок, Внимательный, Тщательный, Аналитик, Гуру Данных", "Способность замечать даже мелкие детали и понимать их важность."); // внимательность

            // Пересекающиеся
            await AddCharacterTraitWithLevelsAsync(new[] { personality1.Id, personality5.Id }, "Дисциплина", "Новичок, Исполнительный, Последовательный, Самодисциплинированный, Организованный", "Способность следовать установленным правилам и процедурам.");
            await AddCharacterTraitWithLevelsAsync(new[] { personality1.Id, personality4.Id, personality5.Id }, "Целеустремленность", "Начинающий, Ученик, Инициатор, Стремящийся, Достигающий", "Способность четко ставить цели и работать на их достижение.");

            await AddCharacterTraitWithLevelsAsync(new[] { personality1.Id, personality6.Id }, "Харизматичность", "Начинающий, Оратор, Публичный Спикер, Вдохновляющий Лидер", "Способность привлекать внимание и вдохновлять других.");
            await AddCharacterTraitWithLevelsAsync(new[] { personality1.Id, personality5.Id }, "Решительность", "Новичок, Студент, Эксперт, Лидер, Вдохновляющий Лидер", "Способность быстро принимать решения и уверенно действовать.");
            await AddCharacterTraitWithLevelsAsync(new[] { personality4.Id, personality7.Id }, "Критическое мышление", "Начинающий, Аналитик, Исследователь, Эксперт, Мыслитель", "Умение анализировать и оценивать информацию с целью принятия обоснованных решений.");
            await AddCharacterTraitWithLevelsAsync(new[] { personality2.Id, personality4.Id }, "Любознательность", "Начинающий, Понимающий, Исследователь, Познаватель, Мыслитель", "Интерес к познанию нового и желание углублять знания.");
            await AddCharacterTraitWithLevelsAsync(new[] { personality3.Id, personality6.Id }, "Эмпатия", "Новичок, Понимающий, Заботливый, Сопереживающий, Доброжелательный", "Способность понимать эмоции и чувства других людей, проявлять сочувствие.");
            await AddCharacterTraitWithLevelsAsync(new[] { personality5.Id, personality7.Id }, "Стратегическое мышление", "Начинающий, Стратег, Эксперт, Гуру Стратегии, Мыслитель", "Умение планировать и предвидеть последствия действий.");
            await AddCharacterTraitWithLevelsAsync(new[] { personality4.Id, personality5.Id }, "Настойчивость", "Новичок, Студент, Упорный, Эксперт, Научный Исследователь", "Способность не сдаваться перед трудностями и упорно двигаться к цели.");
        }

        private async Task AddCharacterTraitWithLevelsAsync(int[] personalityIds, string traitTitle, string levelTitles, string description)
        {
            var characterTrait = await _characterTraitsService.AddAsync(personalityIds, new CharacterTrait(traitTitle) { Description = description });
            var levelTitleArray = levelTitles.Split(", ");
            for (int i = 0; i < levelTitleArray.Length; i++)
            {
                await _characterTraitsService.AddLevelAsync(new CharacterTraitLevel(levelTitleArray[i], characterTrait.Id, i + 1, (i + 1) * 100) { });
            }
        }
    }
}
