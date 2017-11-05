![PG Demidov Yaroslavl State University](https://upload.wikimedia.org/wikipedia/ru/2/28/Logo_demidovskiy_universitet.png)
# ЯрГУ им. П.Г.Демидова
[![Build Status](https://travis-ci.org/YarGU-Demidov/math-site.svg?branch=rc-1.0.0)](https://travis-ci.org/YarGU-Demidov/math-site)
[![Build status](https://ci.appveyor.com/api/projects/status/6uowqp6ypc6o1of3/branch/rc-1.0.0?svg=true)](https://ci.appveyor.com/project/mokeev1995/math-site/branch/rc-1.0.0)
## Сайт математического факультета (обновленный)

### Info

Это сайт для матфака ЯрГУ им. П.Г.Демидова 

Любые pull request-ы, содержащие bugfix-ы и новые полезные features приветствуются

### Требования к системе

* Наличие git
* Наличие .Net Core 2.0 или новее
* Установленный PostgreSQL 9.5 или новее
* Наличие Yarn Package Manager

### Информация по установке

* Склонировать (одну из версий, выбор зависит от типа конфигурации аутентификации гита)
  * `git clone https://github.com/YarGU-Demidov/math-site.git`
  * `git clone git@github.com:YarGU-Demidov/math-site.git`
* если нужна самая последняя версия - брать из последней rc-ветки

#### Вариант 1 (Ручной, из коммандной строки)

* Создать базу данных для сайта
  * Открыть `powershell`/`cmd`/`terminal` (что угодно, лишь бы был доступ к консольной утилите dotnet)
  * Перейти в корневой каталог проекта
  * Запустить `dotnet restore`
  * Перейти в `src/MathSite`
  * Поправить `appsettings.{env}.json`, где env -- это может быть dev или этого пункта может не быть вовсе, то есть просто `appsettings.json`
  * Запустить `dotnet ef database update`
* Добавить данные в БД
  * Перейти в `powershell`/`cmd`/`terminal` в каталог проекта
  * Перейти в `src/MathSite`
  * Запустить команду `dotnet run -с Release --launch-profile MathSite.Seed`
  
* Запустить `dotnet run`

#### Вариант 2 (Автоматический, из Visual Studio)

* Создать базу данных для сайта
  * Открыть проект в `Visual Studio 2017`
  * Поправить `appsettings.{env}.json`, где env - это может быть dev или этого пункта может не быть вовсе, то есть просто `appsettings.json`
  * Открыть окошко `Консоль диспетчера пакетов`
  * Запустить `Update-Database`
* Добавить данные в БД
  * Выбрать конфигурацию `MathSite.Seed` в списке конфигураций запуска сайта.
  * Запустить приложение.

#### Готово :)
  
* И теперь вы можете зайти на `localhost:5000` и использовать сайт!)

### Contributors

* [Mokeev Andrey](http://mokeev1995.ru) \< andrey@mokeev1995.ru >
* [Devyatkin Andrey](https://vk.com/id16824326)

### Copyright and License

MIT Licence
