

# :eyes: Overview

This repository contains a web app for working with events and registering participants for these events.
The application can be launched by following the instructions below.

## Startup instructions

> [!NOTE]
> This method uses Docker, so make sure you have Docker and Docker Compose installed on your machine.

1. Clone repository:

```bash
git clone https://github.com/anticlown322/Events-Web-Application
```

2. Go to the folder with docker file

```bash
cd Events-Web-Application/backend
```

3. Run docker compose for building and starting containers

```bash
docker-compose up -d
```

Option `-d` Allows you to run containers in the background.

4. Once the containers have been successfully launched, the application will be available at:

```bash
http://localhost:8080/swagger/index.html
```

## First steps

Create an admin using `api\authenticate` POST request in Swagger. Example credentials:
```
{
  "firstName": "string",
  "lastName": "string",
  "userName": "string",
  "password": "123456qwerty",
  "email": "string@gmail.com",
  "phoneNumber": "+375336216209",
  "roles": [
    "Administrator"
  ]
}
```

![RegisterUser request for Auth controller](/assets/img/first_steps_1.png)

Once admin has been successfully created, you can login by credentials, provided in example data:
```
{
  "userName": "string",
  "password": "123456qwerty"
}
```

![Login request for Auth controller](/assets/img/first_steps_2.png)

In server response section you will see two tokens:
- `accessToken` for authorization. It is necessary for making requests.
- `refreshToken` for refreshing your accessToken, if it is about to expire. 

Access token expires after 30 minutes from the moment of its creation. 
Use `refreshToken` via `api/authentication/refresh` endpoint.

![AccessToken and refreshToken values](/assets/img/first_steps_3.png)

Authorize with `accessToken` using Swagger `Authorize` button.

![Click swagger auth button](/assets/img/first_steps_4.png)
![Insert auth token in swagger](/assets/img/first_steps_5.png)

Try to send some requests(i.e. create an event or get list of all events).
Check if `Authorization header` is correct. Its value must consist of `Bearer` and value of your `accessToken`. Then check server response value.

![Login request for Auth controller](/assets/img/first_steps_6.png)

If you need to stop the containers, run the command:

```bash
docker-compose down
```

## Fixes (edits by 17.02.2025)

### *1. эндпоинт регистрации возвращает ошибку {"StatusCode":500,"Message":"42P01: relation \u0022AspNetUsers\u0022 does not exist\n\nPOSITION: 368"}*

Ошибка была связана с неверным процессом миграции. В репозиторий файл миграции не включался из-за 
`.gitignore`, а при запуске новая миграция не создавалась и следующий код в `Program.cs` не мог произвести миграцию:

```csharp
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<RepositoryContext>();

    dbContext.Database.Migrate();
}
```

Теперь миграция включена в репозиторий, ошибка исправлена, 
эндпоинт корректно отвечает на запрос.  

### *2. не использовать дата аннотации в домене, вместо этого использовать fluent API*

Исправлено, теперь в моделях из Domain нет дата аннотаций, описание моделей происходит в файлах из 
`Infrastructure.Repository.Config`. Например, `EventsConfig.cs`:
```csharp
public class EventsConfig : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Id)
            .HasColumnName("EventId")
            .ValueGeneratedOnAdd();

        //... and so on
    }
}
```

### *3. ErrorDetails вынести из домена*

Исправлено, `ErrorDetails` перенесено в `Infrastructure`.

### *4. публичные методы репозитория не должны возвращать IQueryable*

Исправлено, теперь методы возвращают `Task<IEnumerable<T>>`. Контракт репозитория исправлен на следующий:
```csharp
public interface IRepositoryBase<T>
{
    Task<IEnumerable<T>> FindAllAsync
        (bool trackChanges, CancellationToken cancellationToken); 

    Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> 
        expression, bool trackChanges, CancellationToken cancellationToken);
    
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}
```

### *5. доменный слой и слой бизнес логики не должны зависеть от infrastructure*

Исправлено, теперь:
- `Domain` не имеет зависимостей от других слоев.
- `Application` зависит только от `Domain`.
- `Infrastructure` зависит от `Application`.
- `Presentation` зависит от `Infrastructure`.
- `Tests` зависит от `Infrastructure`.

### *6. добавить cancellation token на всех уровнях*

Исправлено, теперь в repositories, use cases, controllers и services используются `Cancellation token`.
Контракт репозитория описан выше.

### *7. BadRequest, Unauthorized возвращать через middleware, контроллеры должны возвращать только 2\*\* статус коды*

Исправлено, теперь контроллеры возвращают только:
- `OK(200)`, 
- `NoContent(204)`, 
- `File(200)`, 
- `CreatedAtRoute(201)`.

### *8. для чего разделение слоев архитектуры на несколько отдельных проектов?*

Сложно и неприятно исполнять правки, поставленные в виде вопроса и не имеющие четких указаний, но я попытался. 
Теперь в проекте есть одно решение, которое состоит из 5 сборок:
- `Domain`
- `Application`
- `Infrastructure`
- `Presentation`
- `Tests`

### *9. разнести профили маппера по разным файлам*

Исправлено, теперь в `Application.DTO.MappingProfiles` 3 папки с набором профилей:
- `Event` (3 файла)
- `Participant` (2 файла)
- `User` (1 файл)

### *10. работа с изображением должна быть вынесена в отдельный сервис на слое infrastructure, бизнес логика получения изображения для события должна быть реализована в соответствующем юзкейсе*

Исправлено, теперь:
- `ImageService` находится в `Infrastructure`, 
- для получения изображения создан `GetImageUseCase`, 
- в контроллере `ImageController` используется `GetImageUseCase`.

### *11. генерация и валидация токенов должна проводиться в отдельном сервисе на слое infrastructure, бизнес логика связанная с токенами должна быть реализована в соответствующем юзкейсе*

Исправлено, теперь: 
- генерация и валидация токенов происходит в `AuthenticationManager`, 
- `AuthenticationManager` находится в `Infrastructure`,
- для токенов созданы `CreateTokenForAuthUseCase`, `RefreshTokenForAuthUseCase`, `RegisterUserUseCase`.
- В контроллерах `AuthenticationController` и `TokenController` используются use cases.  

### *12. при создании пользователя нет проверки на уникальность username*

Исправлено, `RegisterUserUseCase` исправлен на следующий:

```csharp
public async Task<IdentityResult> ExecuteAsync(UserForRegistrationDto userForRegistration)
{
    var existingUser = await userManager
        .FindByNameAsync(userForRegistration.UserName);
    
    if (existingUser != null)
    {
        throw new UserAlreadyExistsException(userForRegistration.UserName);
    }
    
    var user = mapper.Map<User>(userForRegistration);
    var result = await userManager.CreateAsync(user, userForRegistration.Password);
    
    if (result.Succeeded)
        await userManager.AddToRolesAsync(user, userForRegistration.Roles);
    
    return result;
}
```

Теперь перед регистраций происходит поиск существующего пользователя с переданным `UserName`.