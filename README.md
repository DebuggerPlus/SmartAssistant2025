# SmartAssistant2025

WPF Smart Assistant с интеграцией OpenAI Assistant API, автоматизацией через Make и локальными задачами.

## Правила для AI ассистента

### Автоматическая очистка детальных логов
- **Периодически анализируй** файл `%APPDATA%\SmartAssistant\Automation\logs\detailed-logs.json`
- **Очищай детальные логи** после анализа проблем или каждые 24 часа
- **Сохраняй важную информацию** перед очисткой, если это необходимо
- **Основные логи НЕ очищай** - они предназначены для пользователя

## Архитектура проекта

Проект состоит из трех модульных подпроектов:

1. **SmartAssistant.Chat** - WPF интерфейс + OpenAI Assistant API
2. **SmartAssistant.Automation** - интеграция с онлайн-сервисами через Make
3. **SmartAssistant.LocalAgent** - локальные задачи и автоматизация действий с мышью

## Технологический стек

- **Backend**: .NET 8.0, C# 12
- **Frontend**: WPF с Material Design
- **AI**: OpenAI Assistant API
- **Автоматизация**: Make (Integromat)
- **Архитектура**: MVVM, Dependency Injection, Clean Architecture

## Быстрый старт

### Предварительные требования

1. .NET 8.0 SDK
2. Visual Studio 2022 или Rider
3. OpenAI API ключ
4. GitHub Personal Access Token (для MCP серверов)

### Настройка

1. **Клонируйте репозиторий:**
   ```bash
   git clone https://github.com/DebuggerPlus/SmartAssistant2025.git
   cd SmartAssistant2025
   ```

2. **Настройте конфигурацию:**
   - Скопируйте `SmartAssistant.Chat/appsettings.example.json` в `SmartAssistant.Chat/appsettings.json`
   - Замените `YOUR_OPENAI_API_KEY_HERE` на ваш OpenAI API ключ
   - Замените `YOUR_ASSISTANT_ID_HERE` на ID вашего OpenAI Assistant

3. **Настройте токены (автоматически):**
   ```bash
   # Запустите скрипт для автоматической настройки всех токенов
   .\tools\setup-credentials.ps1
   ```

   Или настройте вручную:
   - Скопируйте `.cursor/mcp.example.json` в `.cursor/mcp.json`
   - Замените `YOUR_GITHUB_TOKEN_HERE` на ваш GitHub Personal Access Token

4. **Соберите и запустите проект:**
   ```bash
   dotnet build
   dotnet run --project SmartAssistant.Chat
   ```

## Структура проекта

```
SmartAssistant2025/
├── SmartAssistant.Chat/          # WPF приложение с OpenAI интеграцией
│   ├── Models/                   # Модели данных
│   ├── Services/                 # Сервисы (OpenAI, логирование)
│   ├── Views/                    # WPF представления
│   └── Resources/                # Ресурсы приложения
├── SmartAssistant.Automation/    # Модуль автоматизации через Make
├── SmartAssistant.LocalAgent/    # Локальные задачи и автоматизация действий с мышью
└── docs/                         # Документация проекта
```

## Система логирования

### Основные логи (для отображения в окне)
- Сохраняются в `%APPDATA%\SmartAssistant\Automation\logs\main-logs.json`
- Содержат основные события с маскировкой токенов
- Отображаются в реальном времени в окне логирования
- Очищаются кнопкой "Clear Logs" в окне логирования

### Детальные логи (для анализа)
- Сохраняются в `%APPDATA%\SmartAssistant\Automation\logs\detailed-logs.json`
- Содержат подробную информацию для отладки
- Автоматически очищаются AI ассистентом при анализе
- Ограничены 1000 последними записями

### Маскировка токенов
- Токены отображаются в формате: `ABC***XYZ` (первые 3 + последние 3 символа)
- Полные токены никогда не записываются в логи
- Безопасность обеспечивается на уровне утилиты `TokenMasker`

## Безопасность

⚠️ **ВАЖНО**: Никогда не коммитьте секретные данные в Git!

### Хранение токенов

Токены хранятся в файле `docs/credentials.md` (добавлен в `.gitignore`):

```markdown
## GitHub Personal Access Token
```
ghp_your_token_here
```

## OpenAI API Key
```
sk-your-api-key-here
```
```

### Автоматическая настройка

Используйте скрипт для автоматической настройки токенов:

```bash
.\tools\setup-credentials.ps1
```

Этот скрипт:
- Читает токены из `docs/credentials.md`
- Обновляет `.cursor/mcp.json` с GitHub токеном
- Обновляет `SmartAssistant.Chat/appsettings.json` с OpenAI ключами

### Ручная настройка

- API ключи и токены должны храниться в локальных файлах конфигурации
- Файлы с секретами добавлены в `.gitignore`
- Используйте переменные окружения для продакшн окружения

## Разработка

### Принципы

- Следуйте принципам SOLID
- Используйте async/await для асинхронных операций
- Применяйте MVVM паттерн для WPF
- Используйте Dependency Injection

### Сборка и тестирование

```bash
# Сборка всех проектов
dotnet build

# Запуск тестов
dotnet test

# Запуск приложения
dotnet run --project SmartAssistant.Chat
```

## Лицензия

MIT License

## Поддержка

Для вопросов и предложений создавайте issues в GitHub репозитории.