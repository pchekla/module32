# CoreStartApp - Блог с системой логирования

Веб-приложение на ASP.NET Core для ведения блога с функциями регистрации пользователей, обратной связи и системой логирования запросов.

## 🚀 Функциональность

- 👥 Управление пользователями (добавление, просмотр, удаление)
- 💬 Система обратной связи
- 📝 Логирование всех запросов к приложению
- 🎨 Современный адаптивный дизайн

## 📋 Требования

- .NET 7.0 или выше
- SQL Server (LocalDB для Windows или SQL Server Express)
- Visual Studio 2022, VS Code или Rider

## 🛠 Установка и настройка

### Windows

1. Клонируйте репозиторий:
```bash
git clone https://github.com/yourusername/CoreStartApp.git
cd CoreStartApp
```

2. Создайте базу данных в SQL Server:
```sql
sqlcmd -S .\SQLEXPRESS -Q "CREATE DATABASE BlogDb"
```

3. Запустите приложение:
```bash
dotnet restore
dotnet run
```

### macOS

1. Клонируйте репозиторий:
```bash
git clone https://github.com/yourusername/CoreStartApp.git
cd CoreStartApp
```

2. Установите SQL Server для macOS:
```bash
# Установите Docker, если его еще нет
brew install --cask docker

# Запустите SQL Server в контейнере
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong!Passw0rd" \
   -p 1433:1433 --name sql1 \
   -d mcr.microsoft.com/mssql/server:2022-latest
```

3. Создайте базу данных:
```bash
docker exec -it sql1 /opt/mssql-tools/bin/sqlcmd \
   -S localhost -U sa -P YourStrong!Passw0rd \
   -Q "CREATE DATABASE BlogDb"
```

4. Обновите строку подключения в `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BlogDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;"
  }
}
```

5. Запустите приложение:
```bash
dotnet restore
dotnet run
```

## 🌐 Использование

После запуска приложение будет доступно по адресу:
- http://localhost:5000
- https://localhost:5001 (если включен SSL)

### Основные разделы:

- **Главная** - Домашняя страница приложения
- **Пользователи** - Список зарегистрированных пользователей
- **Регистрация** - Форма регистрации новых пользователей
- **Обратная связь** - Форма для отправки отзывов
- **Логи** - История запросов к приложению

## 🔧 Разработка

### Создание миграций

```bash
# Windows
dotnet ef migrations add InitialCreate
dotnet ef database update

# macOS
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Очистка базы данных

```bash
# Windows
dotnet ef database drop -f

# macOS
dotnet ef database drop -f
```

## ⚠️ Важные замечания

- Перед первым запуском убедитесь, что SQL Server запущен и доступен
- Для Windows рекомендуется использовать SQL Server Express или LocalDB
- Для macOS рекомендуется использовать Docker с SQL Server
- Все пароли в примерах нужно заменить на безопасные в продакшен-окружении